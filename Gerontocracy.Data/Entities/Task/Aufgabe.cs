using Gerontocracy.Data.Entities.Account;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gerontocracy.Data.Entities.Task
{
    public class Aufgabe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public TaskType TaskType { get; set; }

        public string Beschreibung { get; set; }

        public DateTime EingereichtAm { get; set; }

        public bool Erledigt { get; set; }

        public string MetaData { get; set; }

        [ForeignKey(nameof(Einreicher))]
        public long EinreicherId { get; set; }
        public User Einreicher { get; set; }

        [ForeignKey(nameof(Bearbeiter))]
        public long? BearbeiterId { get; set; }
        public User Bearbeiter { get; set; }
    }
}
