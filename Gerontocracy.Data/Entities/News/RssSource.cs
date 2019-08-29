using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gerontocracy.Data.Entities.Party;

namespace Gerontocracy.Data.Entities.News
{
    public class RssSource
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Url { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }

        public ICollection<Artikel> Artikel { get; set; }

        public long ParlamentId { get; set; }
        public Parlament Parlament { get; set; }
    }
}
