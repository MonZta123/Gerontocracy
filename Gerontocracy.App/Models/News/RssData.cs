namespace Gerontocracy.App.Models.News
{
    /// <summary>
    /// Reflects the data, which is required to add a new RSS source
    /// </summary>
    public class RssData
    {
        /// <summary>
        /// Url of RSS Feed
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Name of RSS Feed
        /// </summary>
        public string Name { get; set; }
    }
}
