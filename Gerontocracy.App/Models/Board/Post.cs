using System;
using System.Collections.Generic;

namespace Gerontocracy.App.Models.Board
{
    /// <summary>
    /// Single post of a thread
    /// </summary>
    public class Post
    {
        /// <summary>
        /// Id of the post
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// When the post was created
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Id of author
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// Name of Author
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Number of likes
        /// </summary>
        public int Likes { get; set; }

        /// <summary>
        /// the user's like or dislike
        /// </summary>
        public LikeType? UserLike { get; set; }

        /// <summary>
        /// Number of dislikes
        /// </summary>
        public int Dislikes { get; set; }

        /// <summary>
        /// Childposts
        /// </summary>
        public ICollection<Post> Children { get; set; }
    }
}
