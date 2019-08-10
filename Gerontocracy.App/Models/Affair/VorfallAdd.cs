using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Gerontocracy.App.Models.Shared;

namespace Gerontocracy.App.Models.Affair
{
    /// <summary>
    /// Describes the dataset for adding a new entry
    /// </summary>
    public class VorfallAdd
    {
        /// <summary>
        /// Title
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Titel { get; set; }

        /// <summary>
        /// Description of affair
        /// </summary>
        [Required]
        [MaxLength(4000)]
        public string Beschreibung { get; set; }

        /// <summary>
        /// Is it good, neutral or bad?
        /// </summary>
        public ReputationType? ReputationType { get; set; }

        /// <summary>
        /// Who's the son-of-a?
        /// </summary>
        public long? PolitikerId { get; set; }

        /// <summary>
        /// Sources
        /// </summary>
        public List<QuelleBase> Quellen { get; set; }
    }
}
