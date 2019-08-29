using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gerontocracy.Data.Entities.News;
using Gerontocracy.Data.Entities.Party;

namespace Gerontocracy.Data.Entities.Party
{
    public class Parlament
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [StringLength(2)]
        [Required]
        public string Code { get; set; }
        
        [Required]
        public string Langtext { get; set; }

        public ICollection<Partei> Parteien { get; set; }

        public ICollection<RssSource> Sources { get; set; }
    }
}
