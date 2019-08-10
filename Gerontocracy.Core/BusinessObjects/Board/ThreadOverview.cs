using System;

namespace Gerontocracy.Core.BusinessObjects.Board
{
    public class ThreadOverview
    {
        public long Id { get; set; }
        public string Titel { get; set; }
        public long? VorfallId { get; set; }
        public string VorfallTitel { get; set; }
        public long? PolitikerId { get; set; }
        public string PolitikerName { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? UserId { get; set; }
        public string UserName { get; set; }
        public long NumPosts { get; set; }
        public bool Generated { get; set; }
    }
}
