using System;

namespace Gerontocracy.App.Models.Board
{
    /// <summary>
    /// A thread to a topic
    /// </summary>
    public class ThreadDetail
    {
        /// <summary>
        /// The identifier of the thread
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Titel { get; set; }

        /// <summary>
        /// Affair Id
        /// </summary>
        public long? VorfallId { get; set; }

        /// <summary>
        /// Title of Affair
        /// </summary>
        public string VorfallTitel { get; set; }

        /// <summary>
        /// Id of politician who was involved
        /// </summary>
        public long? PolitikerId { get; set; }

        /// <summary>
        /// Name of politician who was involved
        /// </summary>
        public string PolitikerName { get; set; }
        
        /// <summary>
        /// Initial post of the thread
        /// </summary>
        public Post InitialPost { get; set; }
    }
}
