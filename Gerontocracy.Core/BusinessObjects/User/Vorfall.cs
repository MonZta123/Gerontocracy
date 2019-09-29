using System;

namespace Gerontocracy.Core.BusinessObjects.User
{
    public class Vorfall
    {
        public long Id { get; set; }
        public string Titel { get; set; }
        public int Reputation { get; set; }
        public DateTime ErstelltAm { get; set; }

        public long? PolitikerId { get; set; }
        public string PolitikerName { get; set; }
    }
}
