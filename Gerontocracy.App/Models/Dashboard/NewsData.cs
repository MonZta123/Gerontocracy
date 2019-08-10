using Gerontocracy.App.Models.Shared;

namespace Gerontocracy.App.Models.Dashboard
{
    /// <summary>
    /// Data required to generate an affair from news
    /// </summary>
    public class NewsData
    {
        /// <summary>
        /// Id of news
        /// </summary>
        public long NewsId { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Beschreibung { get; set; }

        /// <summary>
        /// Politician Id
        /// </summary>
        public long? PolitikerId { get; set; }

        /// <summary>
        /// ReputationType
        /// </summary>
        public ReputationType? ReputationType { get; set; }
    }
}
