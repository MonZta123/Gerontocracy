using System.ComponentModel.DataAnnotations;

namespace Gerontocracy.App.Models.Board
{
    /// <summary>
    /// Contains the required data for a like or dislike
    /// </summary>
    public class LikeData
    {
        /// <summary>
        /// post id
        /// </summary>
        [Required]
        public long PostId { get; set; }

        /// <summary>
        /// action type
        /// </summary>
        public LikeType? LikeType { get; set; }
    }
}
