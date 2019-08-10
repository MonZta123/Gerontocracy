

namespace Gerontocracy.App.Models.Affair
{
    /// <summary>
    /// Object, which contains the data, required for voting
    /// </summary>
    public class VoteData
    {
        /// <summary>
        /// Affair Id
        /// </summary>
        public long VorfallId { get; set; }

        /// <summary>
        /// VoteType
        /// </summary>
        public VoteType? VoteType { get; set; }
    }
}
