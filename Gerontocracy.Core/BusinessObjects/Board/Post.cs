using System;
using System.Collections.Generic;

namespace Gerontocracy.Core.BusinessObjects.Board
{
    public class Post
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }
        
        public long? UserId { get; set; }
        public string UserName { get; set; }
        
        public int Likes { get; set; }
        public int Dislikes { get; set; }

        public LikeType? UserLike { get; set; }

        public long? ParentId { get; set; }

        public List<Post> Children { get; set; }
    }
}
