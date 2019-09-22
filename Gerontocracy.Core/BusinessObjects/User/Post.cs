using System;

namespace Gerontocracy.Core.BusinessObjects.User
{
    public class Post
    {
        public long Id { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }
    }
}
