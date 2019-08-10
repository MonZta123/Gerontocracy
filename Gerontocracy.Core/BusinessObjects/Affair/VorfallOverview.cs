using System;

namespace Gerontocracy.Core.BusinessObjects.Affair
{
    public class VorfallOverview
    {
        public long Id { get; set; }
        public string Titel { get; set; }
        public int Reputation { get; set; }
        public DateTime ErstelltAm { get; set; }

        public long? PolitikerId { get; set; }
        public string PolitikerName { get; set; }

        public long? ParteiId { get; set; }
        public string ParteiName { get; set; }
    }
}
