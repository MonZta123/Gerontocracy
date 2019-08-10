using System;

namespace Gerontocracy.App.Models.Affair
{
    /// <summary>
    /// Affair overview
    /// </summary>
    public class VorfallOverview
    {
        /// <summary>
        /// Identifier of Affair
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Affair Title
        /// </summary>
        public string Titel { get; set; }

        /// <summary>
        /// How famous is the politician because of this affair
        /// </summary>
        public int Reputation { get; set; }

        /// <summary>
        /// Created on
        /// </summary>
        public DateTime ErstelltAm { get; set; }

        /// <summary>
        /// Politician identifier
        /// </summary>
        public long PolitikerId { get; set; }

        /// <summary>
        /// Politician Name
        /// </summary>
        public string PolitikerName { get; set; }

        /// <summary>
        /// Politician Party
        /// </summary>
        public long? ParteiId { get; set; }

        /// <summary>
        /// Party name
        /// </summary>
        public string ParteiName { get; set; }
    }
}
