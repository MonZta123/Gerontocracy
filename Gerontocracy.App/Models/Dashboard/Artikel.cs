using System;

namespace Gerontocracy.App.Models.Dashboard
{
    /// <summary>
    /// Newsarticle for Feed
    /// </summary>
    public class Artikel
    {
        /// <summary>
        /// Id of Article
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Author
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Publication Date
        /// </summary>
        public DateTime? PubDate { get; set; }

        /// <summary>
        /// Affair Id
        /// </summary>
        public long? VorfallId { get; set; }
    }
}