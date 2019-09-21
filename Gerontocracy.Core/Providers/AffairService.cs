using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using Gerontocracy.Core.BusinessObjects.Affair;
using Gerontocracy.Core.BusinessObjects.Party;
using Gerontocracy.Core.Exceptions.Affair;
using Gerontocracy.Core.Interfaces;
using Gerontocracy.Data;

using System.Security.Claims;
using Gerontocracy.Core.BusinessObjects.Shared;
using Gerontocracy.Data.Entities.Board;
using Gerontocracy.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

using db = Gerontocracy.Data.Entities;

namespace Gerontocracy.Core.Providers
{
    public class AffairService : IAffairService
    {
        public AffairService(GerontocracyContext context, IMapper mapper, IAccountService accountService)
        {
            _context = context;
            _mapper = mapper;
            _accountService = accountService;
        }

        private readonly GerontocracyContext _context;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;

        private const string ConstNoFaction = "OK";

        public long AddVorfall(ClaimsPrincipal user, Vorfall vorfall)
        {
            var userId = _accountService.GetIdOfUser(user);

            var vorfallDb = _mapper.Map<db.Affair.Vorfall>(vorfall);
            vorfallDb.UserId = userId;
            vorfallDb.Legitimitaet = new db.Affair.Vote
            {
                UserId = userId,
                VoteType = db.Affair.VoteType.Up
            }.AsList();

            var threadDb = new Thread
            {
                InitialPost = new Post
                {
                    Content = vorfall.Beschreibung,
                    Likes = new Like { LikeType = LikeType.Like, UserId = userId }.AsList(),
                    UserId = userId
                },
                VorfallId = vorfallDb.Id,
                UserId = userId,
                Generated = true,
                Title = vorfall.Titel,
                Vorfall = vorfallDb
            };

            _context.Add(threadDb);
            _context.SaveChanges();

            return vorfallDb.Id;
        }

        public VorfallOverview GetVorfall(long id)
        {
            var vorfall = GetVorfallRaw(id);

            if (vorfall == null)
                throw new AffairNotFoundException();

            var vorfallMapped = new VorfallOverview()
            {
                Id = vorfall.Id,
                Titel = vorfall.Titel,
                ErstelltAm = vorfall.ErstelltAm,
                Reputation = vorfall.Legitimitaet.Count(p =>
                                 (p.VoteType == Data.Entities.Affair.VoteType.Up && p.Vorfall.ReputationType == Data.Entities.Affair.ReputationType.Positive) ||
                                 (p.VoteType == Data.Entities.Affair.VoteType.Down && p.Vorfall.ReputationType == Data.Entities.Affair.ReputationType.Negative)) -
                             vorfall.Legitimitaet.Count(p =>
                                 (p.VoteType == Data.Entities.Affair.VoteType.Up && p.Vorfall.ReputationType == Data.Entities.Affair.ReputationType.Negative) ||
                                 (p.VoteType == Data.Entities.Affair.VoteType.Down && p.Vorfall.ReputationType == Data.Entities.Affair.ReputationType.Positive)),
                PolitikerId = vorfall.PolitikerId,
                PolitikerName = vorfall.Politiker.Name,
                ParteiName = vorfall.Politiker.Partei.Kurzzeichen,
                ParteiId = vorfall.Politiker.ParteiId
            };

            return vorfallMapped;
        }

        public VorfallDetail GetVorfallDetail(ClaimsPrincipal user, long id)
        {
            var vorfall = GetVorfallRaw(id);

            if (vorfall == null)
                throw new AffairNotFoundException();

            VoteType? voteType = null;
            if (_accountService.IsSignedIn(user))
            {
                var userId = _accountService.GetIdOfUser(user);
                var vote = _context.Vote.SingleOrDefault(n => n.UserId == userId && n.VorfallId == id);
                if (vote != null)
                {
                    voteType = _mapper.Map<VoteType?>(vote.VoteType);
                }
            }
            
            var vorfallMapped = new VorfallDetail
            {
                Id = vorfall.Id,
                Titel = vorfall.Titel,
                ErstelltAm = vorfall.ErstelltAm,
                Beschreibung = vorfall.Beschreibung,
                ErstelltVon = _mapper.Map<BusinessObjects.Account.User>(vorfall.User),
                Politiker = _mapper.Map<PolitikerOverview>(vorfall.Politiker),
                Quellen = _mapper.Map<List<QuelleOverview>>(vorfall.Quellen),
                ReputationType = _mapper.Map<ReputationType>(vorfall.ReputationType),
                Reputation = vorfall.Legitimitaet.Count,
                UserVote = voteType
            };

            vorfallMapped.ErstelltVon.Roles = _accountService.GetUserOrDefaultAsync(vorfall.UserId).Result.Roles;

            return vorfallMapped;
        }

        public SearchResult<VorfallOverview> Search(BusinessObjects.Affair.SearchParameters param, int pageSize = 25, int pageIndex = 0)
        {
            var query = _context.Vorfall
                .Include(n => n.Politiker)
                    .ThenInclude(n => n.Partei)
                .Include(n => n.Legitimitaet)
                .AsQueryable();
            
            if (!string.IsNullOrEmpty(param.Name))
                query = query.Where(n => n.Titel.Contains(param.Name, StringComparison.CurrentCultureIgnoreCase));

            if (!string.IsNullOrEmpty(param.ParteiName))
                query = query.Where(n => n.Politiker.Partei.Kurzzeichen.Contains(param.ParteiName, StringComparison.CurrentCultureIgnoreCase));
            
            var dbResult = query.Select(n => new VorfallOverview()
            {
                ErstelltAm = n.ErstelltAm,
                Id = n.Id,
                ParteiId = n.Politiker.ParteiId,
                ParteiName =
                    n.Politiker.Partei != null ?
                    n.Politiker.Partei.Kurzzeichen :
                    n.Politiker != null ?
                    ConstNoFaction :
                    null,
                PolitikerId = n.PolitikerId,
                PolitikerName = n.Politiker.Name,
                Titel = n.Titel,
                Reputation =
                    (n.Legitimitaet.Count(m => m.VoteType == db.Affair.VoteType.Up)
                    - n.Legitimitaet.Count(m => m.VoteType == db.Affair.VoteType.Down))
                    * (n.ReputationType == db.Affair.ReputationType.Negative ? -1 : 1)
            });

            var result = new SearchResult<VorfallOverview>()
            {
                MaxResults = dbResult.Count(),
                Data = dbResult
                    .OrderByDescending(n => n.ErstelltAm)
                    .Skip(pageSize * pageIndex)
                    .Take(pageSize)
                    .ToList()
            };

            return result;
        }

        public long? Vote(ClaimsPrincipal user, long vorfallId, VoteType? type)
        {
            var userId = _accountService.GetIdOfUser(user);

            if (!_context.Vorfall.Any(n => n.Id == vorfallId))
                throw new AffairNotFoundException();

            db.Affair.Vote dbObj = null;

            if (type.HasValue)
            {
                dbObj = _context.Vote.SingleOrDefault(n => n.UserId == userId && n.VorfallId == vorfallId);

                if (dbObj == null)
                {
                    _context.Add(new db.Affair.Vote
                    {
                        UserId = userId,
                        VorfallId = vorfallId,
                        VoteType = _mapper.Map<db.Affair.VoteType>(type),
                    });
                }
                else
                    dbObj.VoteType = _mapper.Map<db.Affair.VoteType>(type);
            }
            else
                _context.Remove(_context.Vote.Single(n => n.UserId == userId && n.VorfallId == vorfallId));

            _context.SaveChanges();

            return dbObj?.Id;
        }

        private db.Affair.Vorfall GetVorfallRaw(long id)
            => _context.Vorfall
               .Include(n => n.User)
               .Include(n => n.Legitimitaet)
               .Include(n => n.Politiker)
                   .ThenInclude(n => n.Partei)
               .Include(n => n.Quellen)
               .SingleOrDefault(n => n.Id == id);
    }
}
