using System;
using Gerontocracy.App.Models.Account;

namespace Gerontocracy.App.Models.Party
{
    /// <summary>
    /// Describes an Affair
    /// </summary>
    public class VorfallData
    {
        /// <summary>
        /// Affair Identifier
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Affair Title
        /// </summary>
        public string Titel { get; set; }

        /// <summary>
        /// Affair Created On
        /// </summary>
        public DateTime ErstelltAm { get; set; }

        /// <summary>
        /// Created By
        /// </summary>
        public User ErstelltVon { get; set; }
    }
}
