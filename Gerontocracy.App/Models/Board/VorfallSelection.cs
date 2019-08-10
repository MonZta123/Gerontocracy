namespace Gerontocracy.App.Models.Board
{
    /// <summary>
    /// Autocomplete data for affairs
    /// </summary>
    public class VorfallSelection
    {
        /// <summary>
        /// id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Uploader
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Uploader Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// title
        /// </summary>
        public string Titel { get; set; }
    }
}
