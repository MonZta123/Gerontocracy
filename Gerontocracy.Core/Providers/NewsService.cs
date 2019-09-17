using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using Gerontocracy.Core.BusinessObjects.News;
using Gerontocracy.Core.BusinessObjects.Shared;
using Gerontocracy.Core.Exceptions.News;
using Gerontocracy.Core.Interfaces;
using Gerontocracy.Data;

using Gerontocracy.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using db = Gerontocracy.Data.Entities;

namespace Gerontocracy.Core.Providers
{
    public class NewsService : INewsService
    {
        public NewsService(IMapper mapper, GerontocracyContext context, IAccountService accountService, ISyncService syncService)
        {
            this._context = context;
            this._mapper = mapper;
            this._accountService = accountService;
            this._syncService = syncService;
        }

        private readonly GerontocracyContext _context;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        private readonly ISyncService _syncService;

        public List<Artikel> GetLatestNews(int maxResults = 15)
            => _mapper.Map<List<Artikel>>(
             _context.Artikel.OrderByDescending(n => n.PubDate)
             .Take(maxResults)
             .ToList());

        public long GenerateAffair(ClaimsPrincipal user, NewsData data)
        {
            var news = _context.Artikel.SingleOrDefault(n => n.Id == data.NewsId);

            if (news == null)
                throw new NewsNotFoundException(data.NewsId.ToString());

            if (news.VorfallId.HasValue)
                throw new AffairAlreadyAttachedToNewsException(data.NewsId.ToString());

            var userId = _accountService.GetIdOfUser(user);

            news.Vorfall = new db.Affair.Vorfall()
            {
                UserId = userId,
                Beschreibung = news.Description,
                ErstelltAm = DateTime.Now,
                PolitikerId = data.PolitikerId,
                ReputationType = _mapper.Map<db.Affair.ReputationType>(data.ReputationType),
                Titel = news.Title,
                Legitimitaet = new db.Affair.Vote()
                {
                    UserId = userId,
                    VoteType = db.Affair.VoteType.Up
                }.AsList(),
                Quellen = new db.Affair.Quelle()
                {
                    Url = news.Link,
                    Zusatz = "Generiert"
                }.AsList(),
                Threads = new db.Board.Thread()
                {
                    Generated = true,
                    Title = news.Title,
                    UserId = userId,
                    InitialPost = new db.Board.Post()
                    {
                        UserId = userId,
                        Content = data.Beschreibung,
                        Likes = new db.Board.Like() { UserId = userId, LikeType = db.Board.LikeType.Like }.AsList()
                    }
                }.AsList()
            };

            _context.SaveChanges();

            return news.VorfallId.GetValueOrDefault();
        }

        public long AddRssSource(string url, string name, long parlamentId)
        {
            var source = _context.RssSource.SingleOrDefault(n => n.Url.Equals(url, StringComparison.CurrentCultureIgnoreCase));
            if (source != null)
                throw new SourceAlreadyAddedException();

            var newSource = new db.News.RssSource
            {
                Name = name,
                Url = url,
                ParlamentId = parlamentId,
                Enabled = true
            };

            _context.RssSource.Add(newSource);
            _context.SaveChanges();

            _syncService.SyncSource(newSource.Id);

            return newSource.Id;
        }

        public void RemoveRssSource(long id)
        {
            var source = _context.RssSource.SingleOrDefault(n => n.Id == id);
            if (source == null)
                throw new RssSourceNotFoundException();

            source.Enabled = false;
            _context.SaveChanges();
        }

        public SearchResult<Parlament> GetRssSources(string search, int pageSize, int pageIndex)
        {
            var query = _context.RssSource.Include(n => n.Parlament).AsQueryable();

            query = query.OrderBy(n => n.Parlament.Code);

            var count = query.Count();

            query = query.Skip(pageSize * pageIndex)
                .Take(pageSize);

            var data = query
                .ToList()
                .GroupBy(n => n.Parlament, n => n, (key, group) =>
                {
                    key.Sources = group.ToList();
                    return key;
                })
                .ToList();

            return new SearchResult<Parlament>()
            {
                Data = _mapper.Map<List<Parlament>>(data),
                MaxResults = count
            };
        }

        public SearchResult<ParlamentOverview> GetParlaments(string search, int pageSize = 25, int pageIndex = 0)
        {
            var query = _context.Parlament.AsQueryable();
            
            var data = query.Skip(pageSize * pageIndex)
                .Take(pageSize);

            return new SearchResult<ParlamentOverview>()
            {
                Data = _mapper.Map<List<ParlamentOverview>>(data),
                MaxResults = query.Count()
            };
        }
    }
}
