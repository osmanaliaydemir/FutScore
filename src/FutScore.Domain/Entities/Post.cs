using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class Post : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(5000)]
        public string Content { get; set; }

        [StringLength(500)]
        public string? ImageUrl { get; set; }
        [StringLength(500)]
        public string? VideoUrl { get; set; }
        [StringLength(500)]
        public string? Link { get; set; }

        public bool IsPublic { get; set; }
        public bool IsPinned { get; set; }
        public bool IsArchived { get; set; }

        public int ViewCount { get; set; }

        // Navigation properties
        public virtual User? User { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }
        public virtual ICollection<Like>? Likes { get; set; }
        public virtual ICollection<Share>? Shares { get; set; }
    }
} 