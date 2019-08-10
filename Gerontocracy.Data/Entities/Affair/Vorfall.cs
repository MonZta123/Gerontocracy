using Gerontocracy.Data.Entities.Account;
using Gerontocracy.Data.Entities.Party;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gerontocracy.Data.Entities.Board;

namespace Gerontocracy.Data.Entities.Affair
{
    public class Vorfall
    {
        public Vorfall()
        {
            ErstelltAm = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Titel { get; set; }

        [Required]
        [MaxLength(4000)]
        public string Beschreibung { get; set; }

        public DateTime ErstelltAm { get; set; }

        public ReputationType? ReputationType { get; set; }

        [ForeignKey(nameof(Politiker))]
        public long? PolitikerId { get; set; }
        public Politiker Politiker { get; set; }
        
        [ForeignKey(nameof(User))]
        public long UserId { get; set; }
        public User User { get; set; }

        public ICollection<Vote> Legitimitaet { get; set; }
        public ICollection<Quelle> Quellen { get; set; }
        public ICollection<Thread> Threads { get; set; }
    }
}
