using System;

namespace Gerontocracy.App.Models.User
{
    /// <summary>
    /// Post
    /// </summary>
    public class Post
    {
        /// <summary>
        /// Id of post
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Content of post
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Created on 
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// How many upvotes
        /// </summary>
        public int Likes { get; set; }

        /// <summary>
        /// How many downvotes
        /// </summary>
        public int Dislikes { get; set; }

        /// <summary>
        /// The Thread Id
        /// </summary>
        public long ThreadId { get; set; }
    }
}
