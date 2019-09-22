namespace Gerontocracy.App.Models.Party
{
    /// <summary>
    /// Reflects a Parliament
    /// </summary>
    public class Parlament
    {
        /// <summary>
        /// Identifier of Parliament
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Country Code (AT, DE, EU, US, etc.)
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Langtext { get; set; }
    }
}
