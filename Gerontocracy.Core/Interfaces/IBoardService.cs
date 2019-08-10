using System.Collections.Generic;
using System.Security.Claims;
using Gerontocracy.Core.BusinessObjects.Board;
using Gerontocracy.Core.BusinessObjects.Shared;

namespace Gerontocracy.Core.Interfaces
{
    public interface IBoardService
    {
        ThreadDetail GetThread(ClaimsPrincipal user, long id);
        
        SearchResult<ThreadOverview> Search(SearchParameters parameters, int pageSize = 25, int pageIndex = 0);

        long AddThread(ClaimsPrincipal user, ThreadData data);

        void Like(ClaimsPrincipal user, long postId, LikeType? type);

        Post Reply(ClaimsPrincipal user, PostData data);
        List<VorfallSelection> GetFilteredByName(string searchString, int take = 5);
    }
}
