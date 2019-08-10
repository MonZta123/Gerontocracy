namespace Gerontocracy.Core.BusinessObjects.Sync
{
    internal class Politiker
    {
        public long ExternalId { get; set; }

        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string AkadGradPre { get; set; }
        public string AkadGradPost { get; set; }
        public string Wahlkreis { get; set; }
        public string Bundesland { get; set; }

        public string ParteiKurzzeichen { get; set; }
    }
}
