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
        /// Firstname
        /// </summary>
        public string Vorname { get; set; }

        /// <summary>
        /// Lastname
        /// </summary>
        public string Nachname { get; set; }

        /// <summary>
        /// Title before name
        /// </summary>
        public string AkadGradPre { get; set; }

        /// <summary>
        /// Title after name
        /// </summary>
        public string AkadGradPost { get; set; }

        /// <summary>
        /// Election circle
        /// </summary>
        public string Wahlkreis { get; set; }

        /// <summary>
        /// State
        /// </summary>
        public string Bundesland { get; set; }

        /// <summary>
        /// Party Id
        /// </summary>
        public long? ParteiId { get; set; }

        /// <summary>
        /// Currently active
        /// </summary>
        public bool NotActive { get; set; }

        /// <summary>
        /// Reputation of Politician
        /// </summary>
        public long Reputation { get; set; }
    }
}
