using System.Collections.Generic;
using System.Security.Claims;
using Gerontocracy.Core.BusinessObjects.News;

namespace Gerontocracy.Core.Interfaces
{
    public interface INewsService
    {
        List<Artikel> GetLatestNews(int maxResults = 15);

        long GenerateAffair(ClaimsPrincipal user, NewsData data);
    }
}
