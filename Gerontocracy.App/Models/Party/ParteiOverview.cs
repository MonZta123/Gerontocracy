namespace Gerontocracy.App.Models.Party
{
    /// <summary>
    /// Party Overview
    /// </summary>
    public class ParteiOverview
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
        /// Reputation
        /// </summary>
        public int Reputation { get; set; }
    }
}
