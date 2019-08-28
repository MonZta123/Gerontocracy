using Gerontocracy.Data.Entities.Affair;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gerontocracy.Data.Entities.Global;

namespace Gerontocracy.Data.Entities.Party
{
    public class Politiker
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long ExternalId { get; set; }

        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string AkadGradPre { get; set; }
        public string AkadGradPost { get; set; }
        public string Wahlkreis { get; set; }
        public string Bundesland { get; set; }

        [ForeignKey(nameof(Partei))]
        public long? ParteiId { get; set; }
        public Partei Partei { get; set; }

        [ForeignKey(nameof(Parlament))]
        public long? ParlamentId { get; set; }
        public Parlament Parlament { get; set; }

        public Collection<Vorfall> Vorfaelle { get; set; }

        [NotMapped]
        public string Name => $"{Vorname} {Nachname}";

        [NotMapped]
        public string TitelName => $"{AkadGradPre} {Name} {AkadGradPost}";
    }
}
