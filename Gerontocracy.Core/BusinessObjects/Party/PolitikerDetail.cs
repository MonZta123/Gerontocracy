using Gerontocracy.Core.BusinessObjects.Affair;
using System.Collections.Generic;

namespace Gerontocracy.Core.BusinessObjects.Party
{
    public class PolitikerDetail
    {
        public long Id { get; set; }
        public long ExternalId { get; set; }

        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string AkadGradPre { get; set; }
        public string AkadGradPost { get; set; }
        public string Wahlkreis { get; set; }
        public string Bundesland { get; set; }
        public bool NotActive { get; set; }

        public bool IsNationalrat { get; set; }
        public bool IsRegierung { get; set; }

        public int ReputationUp { get; set; }
        public int ReputationDown { get; set; }

        public long? ParteiId { get; set; }
        public ParteiOverview Partei { get; set; }
        public IEnumerable<VorfallData> Vorfaelle { get; set; }
    }
}
