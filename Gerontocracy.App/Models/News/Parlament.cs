using System.Collections.Generic;

namespace Gerontocracy.App.Models.News
{
    /// <summary>
    /// Parlament News
    /// </summary>
    public class Parlament
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Langtext { get; set; }

        /// <summary>
        /// News
        /// </summary>
        public List<RssSource> Sources { get; set; }
    }
}
