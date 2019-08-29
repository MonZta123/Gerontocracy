using System;
using System.Collections.Generic;
using System.Linq;

using Gerontocracy.Core.BusinessObjects.Account;
using Gerontocracy.Core.BusinessObjects.Party;
using Gerontocracy.Core.BusinessObjects.Shared;
using Gerontocracy.Core.Exceptions.Party;
using Gerontocracy.Core.Interfaces;

using Gerontocracy.Data;
using Gerontocracy.Data.Entities.Affair;

using Gerontocracy.Shared.Extensions;

using Microsoft.EntityFrameworkCore;
using ReputationType = Gerontocracy.Data.Entities.Affair.ReputationType;

namespace Gerontocracy.Core.Providers
{
    public class PartyService : IPartyService
    {
        #region Fields

        private const string ConstNoFactionLongname = "Ohne Fraktion";

        private const string ConstNoFactionShortname = "OK";

        private readonly GerontocracyContext _context;

        #endregion Fields

        #region Constructors

        public PartyService(GerontocracyContext context)
        {
            _context = context;
        }

        #endregion Constructors

        #region Methods

        public List<PolitikerSelection> GetFilteredByName(string filterParam, int maxResults = 5)
        {
            var result = _context.Politiker
                .Where(n => n.Name.Contains(filterParam, StringComparison.CurrentCultureIgnoreCase))
                .Take(maxResults)
                .Select(n => new PolitikerSelection()
                {
                    Name = n.Name,
                    Id = n.Id
                })
                .ToList();

            return result;
        }

        public List<Parlament> GetParliaments()
            => _context.Parlament.Select(n => new Parlament()
            {
                Code = n.Code,
                Id = n.Id,
                Langtext = n.Langtext
            }).ToList();

        public ParteiDetail GetParteiDetail(long id) =>
                    id.Equals(0) ?
            GetOkParteiDetail() :
            GetParteiDetailQuery().SingleOrDefault(n => n.Id.Equals(id)) ??
                throw new PartyNotFoundException();

        public ParteiDetail GetParteiDetail(string kurzzeichen) =>
            ConstNoFactionShortname.Equals(kurzzeichen, StringComparison.CurrentCultureIgnoreCase) ?
            GetOkParteiDetail() :
            GetParteiDetailQuery().SingleOrDefault(n => n.Kurzzeichen.Equals(kurzzeichen, StringComparison.CurrentCultureIgnoreCase)) ??
                throw new PartyNotFoundException();

        public List<ParteiOverview> GetParteien()
            => GetParteiOverviewQuery().ToList().Concat(GetOkParteiOverview().AsList()).ToList();

        public List<ParteiDetail> GetParteienDetail()
            => GetParteiDetailQuery().ToList().Concat(GetOkParteiDetail().AsList()).ToList();

        public ParteiOverview GetParteiOverview(long id)
        {
            var data = GetParteiOverviewQuery().SingleOrDefault(n => n.Id == id);
            if (data == null)
                throw new PartyNotFoundException();

            return data;
        }

        public ParteiOverview GetParteiOverview(string kurzzeichen)
        {
            var data = GetParteiOverviewQuery().SingleOrDefault(n => n.Kurzzeichen.Equals(kurzzeichen, StringComparison.CurrentCultureIgnoreCase));
            if (data == null)
                throw new PartyNotFoundException();

            return data;
        }

        public PolitikerOverview GetPolitiker(long id)
        {
            var data = GetPolitikerQuery().SingleOrDefault(n => n.Id == id);
            if (data == null)
                throw new PoliticianNotFoundException();

            return data;
        }

        public PolitikerDetail GetPolitikerDetail(long id)
        {
            var data = GetPolitikerDetailQuery().SingleOrDefault(n => n.Id == id);
            if (data == null)
                throw new PoliticianNotFoundException();

            return data;
        }

        public List<PolitikerOverview> GetPolitikerList()
            => GetPolitikerQuery().ToList();

        public List<ParteiSelection> GetSelection()
            => GetPolitikerSelection(m => true);

        public SearchResult<PolitikerOverview> Search(SearchParameters parameters, int pageSize = 25, int pageIndex = 0)
        {
            var query = _context
                .Politiker
                .Include(n => n.Partei)
                .Include(n => n.Vorfaelle)
                .ThenInclude(n => n.Legitimitaet)
                .AsQueryable();

            if (!string.IsNullOrEmpty(parameters.Name))
                query = query.Where(n => n.Name.Contains(parameters.Name, StringComparison.CurrentCultureIgnoreCase));

            if (!string.IsNullOrEmpty(parameters.ParteiKurzzeichen))
            {
                if (!parameters.ParteiKurzzeichen.Equals(ConstNoFactionShortname, StringComparison.CurrentCultureIgnoreCase))
                {
                    var parteien = _context.Partei.Where(n => n.Kurzzeichen.Contains(parameters.ParteiKurzzeichen, StringComparison.CurrentCultureIgnoreCase));
                    query = query.Where(n => n.Partei != null && parteien.Contains(n.Partei));
                }
                else
                {
                    query = query.Where(n => n.Partei == null);
                }
            }

            var data = query.Select(n => new PolitikerOverview()
            {
                Id = n.Id,
                Bundesland = n.Bundesland,
                ExternalId = n.ExternalId,
                Wahlkreis = n.Wahlkreis,
                Name = n.Name,
                ParteiId = n.ParteiId,
                ParteiKurzzeichen = n.Partei.Kurzzeichen,
                Reputation = n.Vorfaelle.Sum(o =>
                    (o.ReputationType == ReputationType.Positive ? 1 :
                     o.ReputationType == ReputationType.Negative ? -1 : 0) *
                    (o.Legitimitaet.Count(p => p.VoteType == VoteType.Up) -
                     o.Legitimitaet.Count(p => p.VoteType == VoteType.Down)))
            })
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToList();

            var result = new SearchResult<PolitikerOverview>()
            {
                Data = data,
                MaxResults = query.Count()
            };

            return result;
        }

        private ParteiDetail GetOkParteiDetail()
            => new ParteiDetail()
            {
                ExternalId = 0,
                Id = 0,
                Kurzzeichen = ConstNoFactionShortname,
                Name = ConstNoFactionLongname,
                Politiker = GetPolitikerQuery().Where(n => n.ParteiId == null).ToList()
            };

        private ParteiOverview GetOkParteiOverview()
            => new ParteiOverview()
            {
                ExternalId = 0,
                Id = 0,
                Kurzzeichen = ConstNoFactionShortname,
                Name = ConstNoFactionLongname,
                Reputation = 0,
            };

        private ParteiSelection GetOkParteiSelection()
            => new ParteiSelection
            {
                Id = 0,
                Kurzzeichen = ConstNoFactionShortname,
                Politiker = GetPolitikerDetailQuery()
                    .Where(m => m.ParteiId == null)
                    .Select(m => new PolitikerSelection
                    {
                        Id = m.Id,
                        Name = m.Name
                    }).ToList()
            };

        private IQueryable<ParteiDetail> GetParteiDetailQuery()
            => _context.Partei
                .Include(n => n.Politiker)
                .ThenInclude(n => n.Vorfaelle)
                .Select(n => new ParteiDetail()
                {
                    Id = n.Id,
                    ExternalId = n.ExternalId,
                    Kurzzeichen = n.Kurzzeichen,
                    Name = n.Name,
                    Politiker = GetPolitikerQuery().Where(m => m.ParteiId == n.Id)
                });

        private IQueryable<ParteiOverview> GetParteiOverviewQuery()
            => _context.Partei
               .Include(n => n.Politiker)
               .ThenInclude(n => n.Vorfaelle)
               .ThenInclude(n => n.Legitimitaet)
               .Select(n =>
                   new ParteiOverview
                   {
                       Id = n.Id,
                       ExternalId = n.ExternalId,
                       Kurzzeichen = n.Kurzzeichen,
                       Name = n.Name,
                       Reputation = n.Politiker.Sum(m => m.Vorfaelle.Sum(o =>
                           Math.Max(o.Legitimitaet.Count(p => (p.VoteType == VoteType.Up && p.Vorfall.ReputationType == ReputationType.Positive) ||
                                                              (p.VoteType == VoteType.Down && p.Vorfall.ReputationType == ReputationType.Negative)), 0) -
                           Math.Max(o.Legitimitaet.Count(p => (p.VoteType == VoteType.Up && p.Vorfall.ReputationType == ReputationType.Negative) ||
                                                              (p.VoteType == VoteType.Down && p.Vorfall.ReputationType == ReputationType.Positive)), 0)))
                   });

        private IQueryable<PolitikerDetail> GetPolitikerDetailQuery()
         => _context
                .Politiker
                .Include(n => n.Vorfaelle)
                .Select(n => new PolitikerDetail()
                {
                    Id = n.Id,
                    Name = n.Name,
                    Wahlkreis = n.Wahlkreis,
                    ParteiId = n.ParteiId,
                    Partei = GetParteiOverviewQuery().SingleOrDefault(m => m.Id == n.ParteiId) ?? GetOkParteiOverview(),
                    Bundesland = n.Bundesland,
                    ExternalId = n.ExternalId,
                    Vorfaelle = n.Vorfaelle
                        .OrderByDescending(m => m.ErstelltAm)
                        .Take(10)
                        .Select(m => new BusinessObjects.Affair.VorfallData()
                        {
                            Id = m.Id,
                            ErstelltAm = m.ErstelltAm,
                            Titel = m.Titel,
                            ErstelltVon = new User()
                            {
                                Id = m.User.Id,
                                RegisterDate = m.User.RegisterDate,
                                UserName = m.User.UserName
                            }
                        }),
                    ReputationUp = n.Vorfaelle.Sum(o =>
                        Math.Max(o.Legitimitaet.Count(p => (p.VoteType == VoteType.Up && p.Vorfall.ReputationType == ReputationType.Positive) ||
                                                           (p.VoteType == VoteType.Down && p.Vorfall.ReputationType == ReputationType.Negative)), 0)),
                    ReputationDown = n.Vorfaelle.Sum(o =>
                        Math.Max(o.Legitimitaet.Count(p => (p.VoteType == VoteType.Up && p.Vorfall.ReputationType == ReputationType.Negative) ||
                                                           (p.VoteType == VoteType.Down && p.Vorfall.ReputationType == ReputationType.Positive)), 0))
                });

        private IQueryable<PolitikerOverview> GetPolitikerQuery()
            => _context.Politiker
               .Include(n => n.Partei)
               .Include(n => n.Vorfaelle)
               .ThenInclude(n => n.Legitimitaet)
               .Select(n =>
                   new PolitikerOverview
                   {
                       Id = n.Id,
                       ExternalId = n.ExternalId,
                       Name = n.Name,
                       Bundesland = n.Bundesland,
                       ParteiKurzzeichen = n.Partei.Kurzzeichen,
                       ParteiId = n.ParteiId,
                       Wahlkreis = n.Wahlkreis,
                       Reputation = n.Vorfaelle.Sum(o =>
                           Math.Max(o.Legitimitaet.Count(p => (p.VoteType == VoteType.Up && p.Vorfall.ReputationType == ReputationType.Positive) ||
                                                              (p.VoteType == VoteType.Down && p.Vorfall.ReputationType == ReputationType.Negative)), 0) -
                           Math.Max(o.Legitimitaet.Count(p => (p.VoteType == VoteType.Up && p.Vorfall.ReputationType == ReputationType.Negative) ||
                                                              (p.VoteType == VoteType.Down && p.Vorfall.ReputationType == ReputationType.Positive)), 0))
                   });

        private List<ParteiSelection> GetPolitikerSelection(Func<PolitikerOverview, bool> predicate)
            => GetParteiDetailQuery().Select(n => new ParteiSelection()
            {
                Id = n.Id,
                Kurzzeichen = n.Kurzzeichen,
                Name = n.Name,
                Politiker = n.Politiker
                    .Where(predicate)
                    .Select(m => new PolitikerSelection()
                    {
                        Id = m.Id,
                        Name = m.Name,
                    })
                    .ToList()
            })
            .Where(n => n.Politiker.Any())
            .ToList()
            .Concat(GetOkParteiSelection().AsList())
            .ToList();
        #endregion Methods
    }
}
