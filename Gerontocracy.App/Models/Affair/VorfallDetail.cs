using System;
using System.Collections.Generic;

using Gerontocracy.App.Models.Party;
using Gerontocracy.App.Models.Shared;

namespace Gerontocracy.App.Models.Affair
{
    /// <summary>
    /// represents an affair detail dto
    /// </summary>
    public class VorfallDetail
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Represents the title of the affair
        /// </summary>
        public string Titel { get; set; }

        /// <summary>
        /// The affair description
        /// </summary>
        public string Beschreibung { get; set; }

        /// <summary>
        /// When was the affair-entry created
        /// </summary>
        public DateTime ErstelltAm { get; set; }

        /// <summary>
        /// Is this good or bad?
        /// </summary>
        public ReputationType ReputationType { get; set; }

        /// <summary>
        /// How did the User vote?
        /// </summary>
        public VoteType? UserVote { get; set; }

        /// <summary>
        /// Score
        /// </summary>
        public int Reputation { get; set; }

        /// <summary>
        /// Sources
        /// </summary>
        public List<QuelleOverview> Quellen { get; set; }

        /// <summary>
        /// Politician involved
        /// </summary>
        public PolitikerOverview Politiker { get; set; }
    }
}
