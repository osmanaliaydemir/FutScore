using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class Comment : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid PostId { get; set; }

        public Guid? ParentCommentId { get; set; }

        [Required]
        [StringLength(2000)]
        public string Content { get; set; }

        [StringLength(500)]
        public string? ImageUrl { get; set; }
        [StringLength(500)]
        public string? VideoUrl { get; set; }

        public bool IsEdited { get; set; }
        public bool IsHidden { get; set; }

        public int LikeCount { get; set; }

        // Navigation properties
        public virtual User? User { get; set; }
        public virtual Post? Post { get; set; }
        public virtual Comment? ParentComment { get; set; }
        public virtual ICollection<Comment>? Replies { get; set; }
        public virtual ICollection<Like>? Likes { get; set; }
    }
} 