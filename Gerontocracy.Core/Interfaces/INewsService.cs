using System.Collections.Generic;
using System.Security.Claims;
using Gerontocracy.Core.BusinessObjects.News;
using Gerontocracy.Core.BusinessObjects.Shared;

namespace Gerontocracy.Core.Interfaces
{
    public interface INewsService
    {
        List<Artikel> GetLatestNews(int maxResults = 15);

        long GenerateAffair(ClaimsPrincipal user, NewsData data);

        long AddRssSource(string url, string name, long parlamentId);

        void RemoveRssSource(long id);

        SearchResult<Parlament> GetRssSources(string search, int pageSize = 25, int pageIndex = 0);

        SearchResult<ParlamentOverview> GetParlaments(string search, int pageSize = 25, int pageIndex = 0);
    }
}
