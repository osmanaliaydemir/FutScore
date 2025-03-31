using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class UserDevice : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string DeviceId { get; set; }

        [Required]
        [StringLength(50)]
        public string DeviceType { get; set; }

        [StringLength(100)]
        public string? DeviceName { get; set; }

        [StringLength(100)]
        public string? OperatingSystem { get; set; }

        [StringLength(100)]
        public string? Browser { get; set; }

        [StringLength(100)]
        public string? AppVersion { get; set; }

        [StringLength(500)]
        public string? PushToken { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastUsedAt { get; set; }

        public virtual User? User { get; set; }
    }
} 