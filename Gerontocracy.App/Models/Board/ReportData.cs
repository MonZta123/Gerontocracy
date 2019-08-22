using System.ComponentModel.DataAnnotations;

namespace Gerontocracy.App.Models.Board
{
    /// <summary>
    /// Describes the Data required for reporting a post
    /// </summary>
    public class ReportData
    {
        /// <summary>
        /// Id of Post
        /// </summary>
        public long PostId { get; set; }

        /// <summary>
        /// Additional user comment
        /// </summary>
        [MaxLength(4000)]
        public string Comment { get; set; }
    }
}
