using System.Collections.Generic;

namespace Gerontocracy.Core.BusinessObjects.Shared
{
    public class SearchResult<T>
    {
        public List<T> Data { get; set; }
        public int MaxResults { get; set; }
    }
}
