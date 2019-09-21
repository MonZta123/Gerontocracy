namespace Gerontocracy.Core.BusinessObjects.Party
{
    public class SearchParameters
    {
        public string Name { get; set; }
        public string ParteiKurzzeichen { get; set; }
        public bool IncludeInactive { get; set; }
    }
}
