using System.Collections.Generic;
using Gerontocracy.Core.BusinessObjects.Shared;

namespace Gerontocracy.Core.BusinessObjects.Affair
{
    public class Vorfall
    {
        public string Titel { get; set; }
        public string Beschreibung { get; set; }
        public ReputationType? ReputationType { get; set; }
        public long? PolitikerId { get; set; }
        public List<Quelle> Quellen { get; set; }
    }
}
