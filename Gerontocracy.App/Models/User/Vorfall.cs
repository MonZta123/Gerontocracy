using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gerontocracy.App.Models.User
{
    /// <summary>
    /// Affair
    /// </summary>
    public class Vorfall
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
    }
}
