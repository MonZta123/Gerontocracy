using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gerontocracy.Data.Entities.Affair;

namespace Gerontocracy.Data.Entities.News
{
    public class Artikel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        
        [Required]
        public string Identifier { get; set; }

        [Required]
        public string Link { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime? PubDate { get; set; }

        [ForeignKey(nameof(Vorfall))]
        public long? VorfallId { get; set; }
        public Vorfall Vorfall { get; set; }
    }
}
