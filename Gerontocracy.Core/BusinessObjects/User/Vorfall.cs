namespace Gerontocracy.Core.BusinessObjects.User
{
    public class Vorfall
    {
        public long Id { get; set; }

        public string Titel { get; set; }

        public string Beschreibung { get; set; }

        public int Upvotes { get; set; }

        public int Downvotes { get; set; }
    }
}
