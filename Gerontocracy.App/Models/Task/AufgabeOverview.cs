using System;

namespace Gerontocracy.App.Models.Task
{
    /// <summary>
    /// Describes the Overview of a Task
    /// </summary>
    public class AufgabeOverview
    {
        /// <summary>
        /// Id of task
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Type of task
        /// </summary>
        public TaskType TaskType { get; set; }

        /// <summary>
        /// Submission date
        /// </summary>
        public DateTime EingereichtAm { get; set; }

        /// <summary>
        /// username of incident submitter
        /// </summary>
        public string Einreicher { get; set; }

        /// <summary>
        /// Task taken by
        /// </summary>
        public bool Uebernommen { get; set; }

        /// <summary>
        /// Erledigt
        /// </summary>
        public bool Erledigt { get; set; }
    }
}
