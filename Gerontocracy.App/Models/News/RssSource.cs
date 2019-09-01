namespace Gerontocracy.App.Models.News
{
    /// <summary>
    /// Reflects a source of an rss feed
    /// </summary>
    public class RssSource
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Url of Source
        /// </summary>
        public string Url { get; set; }
        
        /// <summary>
        /// Name of Source
        /// </summary>
        public string Name { get; set;  }
    }
}
