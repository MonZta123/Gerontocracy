namespace Gerontocracy.App.Models.Party
{
    /// <summary>
    /// Politician overview
    /// </summary>
    public class PolitikerOverview
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
        /// Party
        /// </summary>
        public string ParteiKurzzeichen { get; set; }

        /// <summary>
        /// State
        /// </summary>
        public string Bundesland { get; set; }

        /// <summary>
        /// Party Id
        /// </summary>
        public long? ParteiId { get; set; }

        /// <summary>
        /// Reputation of Politician
        /// </summary>
        public long Reputation { get; set; }
    }
}
