namespace Gerontocracy.Core.BusinessObjects.Party
{
    public class PolitikerOverview
    {
        public long Id { get; set; }
        public long ExternalId { get; set; }

        public string Name { get; set; }
        public string Wahlkreis { get; set; }
        public string Bundesland { get; set; }
        public bool IsInactive { get; set; }
        public int Reputation { get; set; }

        public long? ParteiId { get; set; }
        public string ParteiKurzzeichen { get; set; }
    }
}
