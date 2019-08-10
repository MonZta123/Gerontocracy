namespace Gerontocracy.Core.BusinessObjects.Board
{
    public class ThreadDetail
    {
        public long Id { get; set; }
        public string Titel { get; set; }
        public long? VorfallId { get; set; }
        public string VorfallTitel { get; set; }
        public long? PolitikerId { get; set; }
        public string PolitikerName { get; set; }
        public Post InitialPost { get; set; }
    }
}
