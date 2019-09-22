using Gerontocracy.Core.BusinessObjects.Affair;
using System.Collections.Generic;

namespace Gerontocracy.Core.BusinessObjects.Party
{
    public class PolitikerDetail
    {
        public long Id { get; set; }
        public long ExternalId { get; set; }

        public string Name { get; set; }
        public string Wahlkreis { get; set; }
        public string Bundesland { get; set; }
        public bool IsInactive { get; set; }

        public int ReputationUp { get; set; }
        public int ReputationDown { get; set; }

        public long? ParteiId { get; set; }
        public ParteiOverview Partei { get; set; }
        public IEnumerable<VorfallData> Vorfaelle { get; set; }
    }
}
