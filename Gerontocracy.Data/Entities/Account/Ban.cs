using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gerontocracy.Data.Entities.Account
{
    public class Ban
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public DateTime BanDate { get; set; }

        public DateTime? BanEnd { get; set; }
        public string Reason { get; set; }

        public DateTime? BanLifted { get; set; }
        public string BanLiftReason { get; set; }

        [ForeignKey(nameof(BannedUser))]
        public long BannedUserId { get; set; }
        public User BannedUser { get; set; }

        [ForeignKey(nameof(BannedBy))]
        public long BannedById { get; set; }
        public User BannedBy { get; set; }

        [ForeignKey(nameof(UnbannedBy))]
        public long? UnbannedById { get; set; }
        public User UnbannedBy { get; set; }
    }
}
