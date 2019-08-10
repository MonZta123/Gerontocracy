using System.ComponentModel.DataAnnotations;

namespace Gerontocracy.App.Models.Board
{
    /// <summary>
    /// Data sent to server for posting a reply
    /// </summary>
    public class PostData
    {
        /// <summary>
        /// The reply's content
        /// </summary>
        [Required]
        [MaxLength(4000)]
        public string Content { get; set; }

        /// <summary>
        /// The parent's id
        /// </summary>
        public long ParentId { get; set; }
    }
}
