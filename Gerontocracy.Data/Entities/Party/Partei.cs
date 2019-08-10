using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gerontocracy.Data.Entities.Party
{
    public class Partei
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long ExternalId { get; set; }

        public string Kurzzeichen { get; set; }
        public string Name { get; set; }

        public ICollection<Politiker> Politiker { get; set; }
    }
}
