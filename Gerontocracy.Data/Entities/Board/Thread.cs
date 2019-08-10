using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gerontocracy.Data.Entities.Account;
using Gerontocracy.Data.Entities.Affair;

namespace Gerontocracy.Data.Entities.Board
{
    public class Thread
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        
        public bool Generated { get; set; }
        
        [ForeignKey(nameof(Vorfall))]
        public long? VorfallId { get; set; }
        public Vorfall Vorfall { get; set; }

        [ForeignKey(nameof(InitialPost))]
        public long InitialPostId { get; set; }
        public Post InitialPost { get; set; }

        [ForeignKey(nameof(User))]
        public long? UserId { get; set; }
        public User User { get; set; }
    }
}
