using System;

namespace Gerontocracy.Core.BusinessObjects.News
{
    public class Artikel
    {
        public long Id { get; set; }

        public string Link { get; set; }
        
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public DateTime? PubDate { get; set; }

        public long? VorfallId { get; set; }
    }
}
