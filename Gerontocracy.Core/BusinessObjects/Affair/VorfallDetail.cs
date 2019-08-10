using Gerontocracy.Core.BusinessObjects.Party;
using System;
using System.Collections.Generic;
using Gerontocracy.Core.BusinessObjects.Shared;

namespace Gerontocracy.Core.BusinessObjects.Affair
{
    public class VorfallDetail
    {
        public long Id { get; set; }

        public string Titel { get; set; }
        public string Beschreibung { get; set; }
        public DateTime ErstelltAm { get; set; }
        public ReputationType? ReputationType { get; set; }
        public int Reputation { get; set; }
        public VoteType? UserVote { get; set; }

        public List<QuelleOverview> Quellen { get; set; }
        public PolitikerOverview Politiker { get; set; }
    }
}
