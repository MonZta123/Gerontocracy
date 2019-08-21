using System;

namespace Gerontocracy.App.Models.Task
{
    /// <summary>
    /// Reflects a detailview of a task
    /// </summary>
    public class AufgabeDetail
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Type of the task
        /// </summary>
        public TaskType TaskType { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Beschreibung { get; set; }

        /// <summary>
        /// Timestamp of submission
        /// </summary>
        public DateTime EingereichtAm { get; set; }

        /// <summary>
        /// Additional info required for the frontend
        /// </summary>
        public string MetaData { get; set; }

        /// <summary>
        /// Is done
        /// </summary>
        public bool Erledigt { get; set; }

        /// <summary>
        /// Id of submitter
        /// </summary>
        public long EinreicherId { get; set; }

        /// <summary>
        /// The user's name who submitted the incident
        /// </summary>
        public string Einreicher { get; set; }

        /// <summary>
        /// The reviser's name
        /// </summary>
        public string Bearbeiter { get; set; }

        /// <summary>
        /// Id of reviser
        /// </summary>
        public long BearbeiterId { get; set; }
    }
}
