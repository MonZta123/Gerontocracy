namespace Gerontocracy.Core.BusinessObjects.Party
{
    public class ParteiOverview
    {
        public long Id { get; set; }
        public long ExternalId { get; set; }

        public string Kurzzeichen { get; set; }
        public string Name { get; set; }

        public int Reputation { get; set; }
    }
}
