using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gerontocracy.Data.Entities.Affair
{
    public class Quelle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Url { get; set; }

        public string Zusatz { get; set; }

        [ForeignKey(nameof(Vorfall))]
        public long VorfallId { get; set; }

        public Vorfall Vorfall { get; set; }
    }
}
