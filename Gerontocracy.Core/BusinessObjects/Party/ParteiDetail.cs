using System.Collections.Generic;

namespace Gerontocracy.Core.BusinessObjects.Party
{
    public class ParteiDetail
    {
        public long Id { get; set; }
        public long ExternalId { get; set; }

        public string Kurzzeichen { get; set; }
        public string Name { get; set; }
        
        public IEnumerable<PolitikerOverview> Politiker { get; set; }
    }
}
