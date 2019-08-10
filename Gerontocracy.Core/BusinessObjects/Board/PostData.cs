using System.ComponentModel.DataAnnotations;

namespace Gerontocracy.Core.BusinessObjects.Board
{
    public class PostData
    {
        public string Content { get; set; }
        public long ParentId { get; set; }
    }
}
