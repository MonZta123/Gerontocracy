using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gerontocracy.Data.Entities.Account;

namespace Gerontocracy.Data.Entities.Board
{
    public class Post
    {
        public Post()
        {
            this.CreatedOn = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [MaxLength(4000)]
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        [ForeignKey(nameof(Parent))]
        public long? ParentId { get; set; }
        public Post Parent { get; set; }

        [ForeignKey(nameof(User))]
        public long? UserId { get; set; }
        public User User { get; set; }

        public ICollection<Post> Children { get; set; }
        public ICollection<Like> Likes { get; set; }
    }
}