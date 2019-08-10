namespace Gerontocracy.App.Models.Party
{
    /// <summary>
    /// View of the politician selection
    /// </summary>
    public class PolitikerSelection
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Firstname
        /// </summary>
        public string Vorname { get; set; }

        /// <summary>
        /// Lastname
        /// </summary>
        public string Nachname { get; set; }

        /// <summary>
        /// Title Prefix
        /// </summary>
        public string AkadGradPre { get; set; }

        /// <summary>
        /// Title Postfix
        /// </summary>
        public string AkadGradPost { get; set; }

        /// <summary>
        /// Is member of council
        /// </summary>
        public bool IsNationalrat { get; set; }

        /// <summary>
        /// Is member of government
        /// </summary>
        public bool IsRegierung { get; set; }
    }
}
