﻿using System.Collections.Generic;

namespace Gerontocracy.App.Models.Party
{
    /// <summary>
    /// Politician detail
    /// </summary>
    public class PolitikerDetail
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// External Identifier
        /// </summary>
        public long ExternalId { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Election circle
        /// </summary>
        public string Wahlkreis { get; set; }

        /// <summary>
        /// State
        /// </summary>
        public string Bundesland { get; set; }

        /// <summary>
        /// Id of the party
        /// </summary>
        public long? ParteiId { get; set; }
        
        /// <summary>
        /// Party
        /// </summary>
        public ParteiOverview Partei { get; set; }

        /// <summary>
        /// Affairs of this politician
        /// </summary>
        public IEnumerable<VorfallData> Vorfaelle { get; set; }

        /// <summary>
        /// Reputation Upvotes
        /// </summary>
        public int ReputationUp { get; set; }

        /// <summary>
        /// Reputation Downvotes
        /// </summary>
        public int ReputationDown { get; set; }
    }
}
