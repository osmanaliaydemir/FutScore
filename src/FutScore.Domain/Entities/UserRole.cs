using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class UserRole : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string RoleName { get; set; }

        public string Description { get; set; }
        public bool IsActive { get; set; }

        public virtual User User { get; set; }
    }
} 