using System;

namespace Gerontocracy.App.Models.User
{
    /// <summary>
    /// A thread to a topic
    /// </summary>
    public class Thread
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
        /// When the post was created
        /// </summary>
        public DateTime CreatedOn { get; set; }
    }
}
