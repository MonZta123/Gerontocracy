namespace Gerontocracy.App.Models.Affair
{
    /// <summary>
    /// Defines the base type for sources
    /// </summary>
    public class QuelleBase
    {
        /// <summary>
        /// Target Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Additional Information
        /// </summary>
        public string Zusatz { get; set; }
    }
}
