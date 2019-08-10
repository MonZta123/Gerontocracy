using System.Security.Claims;
using Gerontocracy.Core.BusinessObjects.Affair;
using Gerontocracy.Core.BusinessObjects.Shared;

namespace Gerontocracy.Core.Interfaces
{
    public interface IAffairService
    {
        VorfallOverview GetVorfall(long id);

        VorfallDetail GetVorfallDetail(ClaimsPrincipal user, long id);

        void AddVorfall(ClaimsPrincipal user, Vorfall vorfall);

        void Vote(ClaimsPrincipal user, long vorfallId, VoteType? type);

        SearchResult<VorfallOverview> Search(SearchParameters param, int pageSize = 25, int pageIndex = 0);

    }
}
