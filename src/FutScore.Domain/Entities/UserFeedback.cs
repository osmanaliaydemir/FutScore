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
        public string Content { get; set; }

        public int Rating { get; set; }
        public string? Category { get; set; }
        public string? Status { get; set; }

        public string? Response { get; set; }
        public DateTime? RespondedAt { get; set; }

        // Navigation property
        public virtual User? User { get; set; }
    }
} 