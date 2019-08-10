using System.ComponentModel.DataAnnotations;

namespace Gerontocracy.App.Models.Board
{
    /// <summary>
    /// Contains the data to create a thread
    /// </summary>
    public class ThreadData
    {
        /// <summary>
        /// Title of Thread
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Titel { get; set; }

        /// <summary>
        /// Content of first post
        /// </summary>
        [Required]
        [MaxLength(4000)]
        public string Content { get; set; }

        /// <summary>
        /// affair Id
        /// </summary>
        public long? VorfallId { get; set; }
    }
}
