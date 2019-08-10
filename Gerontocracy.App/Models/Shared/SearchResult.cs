using System.Collections.Generic;

namespace Gerontocracy.App.Models.Shared
{
    /// <summary>
    /// Represents the result of a search
    /// </summary>
    public class SearchResult<T>
    {
        /// <summary>
        /// List of found objects
        /// </summary>
        public List<T> Data { get; set; }

        /// <summary>
        /// The max number of rows
        /// </summary>
        public int MaxResults { get; set; }
    }
}
