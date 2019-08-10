using Gerontocracy.Data.Entities.Account;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gerontocracy.Data.Entities.Affair
{
    public class Vote
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        
        public VoteType VoteType { get; set; }

        [ForeignKey(nameof(Vorfall))]
        public long VorfallId { get; set; }
        public Vorfall Vorfall { get; set; }
        
        [ForeignKey(nameof(User))]
        public long UserId { get; set; }
        public User User { get; set; }
    }
}
