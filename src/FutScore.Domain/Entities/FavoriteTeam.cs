using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class FavoriteTeam : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid TeamId { get; set; }

        public DateTime AddedAt { get; set; }

        // Navigation Properties
        public virtual User User { get; set; }
        public virtual Team Team { get; set; }
    }
} 