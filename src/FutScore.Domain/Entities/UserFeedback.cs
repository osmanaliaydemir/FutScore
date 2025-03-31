using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class UserFeedback : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string FeedbackType { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [StringLength(2000)]
        public string Content { get; set; }

        public int Rating { get; set; }
        [StringLength(50)]
        public string? Category { get; set; }
        [StringLength(50)]
        public string? Status { get; set; }

        [StringLength(2000)]
        public string? Response { get; set; }
        public DateTime? RespondedAt { get; set; }

        // Navigation property
        public virtual User? User { get; set; }
    }
} 