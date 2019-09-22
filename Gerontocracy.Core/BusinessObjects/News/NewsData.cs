using Gerontocracy.Core.BusinessObjects.Shared;

namespace Gerontocracy.Core.BusinessObjects.News
{
    public class NewsData
    {
        public long NewsId { get; set; }
        public string Beschreibung { get; set; }
        public long? PolitikerId { get; set; }
        public ReputationType? ReputationType { get; set; }
    }
}
