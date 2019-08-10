using System.Collections.Generic;

namespace Gerontocracy.App.Models.Party
{
    /// <summary>
    /// View of the party-selection
    /// </summary>
    public class ParteiSelection
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Shortname
        /// </summary>
        public string Kurzzeichen { get; set; }

        /// <summary>
        /// Longname
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Partymembers
        /// </summary>
        public List<PolitikerSelection> Politiker { get; set; }
    }
}
