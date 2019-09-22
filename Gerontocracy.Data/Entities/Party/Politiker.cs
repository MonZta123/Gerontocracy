﻿using Gerontocracy.Data.Entities.Affair;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gerontocracy.Data.Entities.Party
{
    public class Politiker
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long ExternalId { get; set; }

        public string Name { get; set; }
        public string Wahlkreis { get; set; }
        public string Bundesland { get; set; }
        public bool IsInactive { get; set; }
        
        [ForeignKey(nameof(Partei))]
        public long? ParteiId { get; set; }
        public Partei Partei { get; set; }

        [ForeignKey(nameof(Parlament))]
        public long? ParlamentId { get; set; }
        public Parlament Parlament { get; set; }

        public Collection<Vorfall> Vorfaelle { get; set; }
    }
}
