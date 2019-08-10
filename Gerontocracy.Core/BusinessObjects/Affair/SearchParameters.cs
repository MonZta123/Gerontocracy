using System;

namespace Gerontocracy.Core.BusinessObjects.Affair
{
    public class SearchParameters
    {
        public string Titel { get; set; }
        public int? MinReputation { get; set; }
        public int? MaxReputation { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string ParteiName { get; set; }
    }
}
