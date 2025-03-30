using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class UserRole : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public virtual User? User { get; set; }
        public virtual Role? Role { get; set; }
    }
} 