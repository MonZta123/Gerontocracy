namespace Gerontocracy.Core.BusinessObjects.Sync
{
    internal class Politiker
    {
        public long ExternalId { get; set; }

        public string Name { get; set; }
        public string Wahlkreis { get; set; }
        public string Bundesland { get; set; }

        public string ParteiKurzzeichen { get; set; }
    }
}
