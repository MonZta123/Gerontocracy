using System.Collections.Generic;

namespace Gerontocracy.App.Models.Party
{
    /// <summary>
    /// Party detail
    /// </summary>
    public class ParteiDetail
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// External identifier
        /// </summary>
        public long ExternalId { get; set; }

        /// <summary>
        /// Short name
        /// </summary>
        public string Kurzzeichen { get; set; }

        /// <summary>
        /// Long name
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// List of members
        /// </summary>
        public List<PolitikerOverview> Politiker { get; set; }
    }
}
