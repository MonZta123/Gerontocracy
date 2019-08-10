using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gerontocracy.Data.Entities.Account;

namespace Gerontocracy.Data.Entities.Board
{
    public class Like
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public LikeType LikeType { get; set; }

        [ForeignKey(nameof(Post))]
        public long PostId { get; set; }
        public Post Post { get; set; }

        [ForeignKey(nameof(User))]
        public long UserId { get; set; }
        public User User { get; set; }
    }
}
