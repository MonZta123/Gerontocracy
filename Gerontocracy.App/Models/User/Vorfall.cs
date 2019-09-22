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
        /// Id of affair
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Titel of affair
        /// </summary>
        public string Titel { get; set; }

        /// <summary>
        /// Description of affair
        /// </summary>
        public string Beschreibung { get; set; }

        /// <summary>
        /// How many upvotes
        /// </summary>
        public int Upvotes { get; set; }

        /// <summary>
        /// How many downvotes
        /// </summary>
        public int Downvotes { get; set; }
    }
}
