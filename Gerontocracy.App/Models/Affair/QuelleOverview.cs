namespace Gerontocracy.App.Models.Affair
{
    /// <summary>
    /// Represents an information source
    /// </summary>
    public class QuelleOverview : QuelleBase
    {
        /// <summary>
        /// Identifier of the source
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// Legitimicy
        /// </summary>
        public int Legitimitaet { get; set; }
    }
}
