using Gerontocracy.Core.BusinessObjects.Party;
using Gerontocracy.Core.BusinessObjects.Shared;
using System.Collections.Generic;

namespace Gerontocracy.Core.Interfaces
{
    public interface IPartyService
    {
        List<ParteiOverview> GetParteien();
        List<ParteiDetail> GetParteienDetail();
        ParteiOverview GetParteiOverview(long id);
        ParteiOverview GetParteiOverview(string kurzzeichen);
        ParteiDetail GetParteiDetail(long id);
        ParteiDetail GetParteiDetail(string kurzzeichen);

        PolitikerOverview GetPolitiker(long id);
        PolitikerDetail GetPolitikerDetail(long id);
        List<PolitikerOverview> GetPolitikerList();

        List<ParteiSelection> GetNationalratSelection();
        List<ParteiSelection> GetRegierungSelection();
        List<ParteiSelection> GetSelection();
        List<ParteiSelection> GetInactiveSelection();

        SearchResult<PolitikerOverview> Search(SearchParameters parameters, int pageSize = 25, int pageIndex = 0);

        List<PolitikerSelection> GetFilteredByName(string filterParam, int maxResults = 5);
    }
}
