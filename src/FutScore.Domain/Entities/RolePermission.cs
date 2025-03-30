using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class RolePermission : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string RoleName { get; set; }

        [Required]
        [StringLength(100)]
        public string Permission { get; set; }

        public string Description { get; set; }
        public bool IsActive { get; set; }

        public virtual UserRole UserRole { get; set; }
    }
} 