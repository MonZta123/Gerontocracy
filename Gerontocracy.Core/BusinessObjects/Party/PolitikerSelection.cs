namespace Gerontocracy.Core.BusinessObjects.Party
{
    public class PolitikerSelection
    {
        public long Id { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string AkadGradPre { get; set; }
        public string AkadGradPost { get; set; }

        public bool IsNationalrat { get; set; }
        public bool IsRegierung { get; set; }
    }
}
