using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class UserLocation : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string IpAddress { get; set; }

        [StringLength(100)]
        public string Country { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(100)]
        public string Region { get; set; }

        [StringLength(100)]
        public string TimeZone { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public DateTime LastUpdatedAt { get; set; }
        public bool IsActive { get; set; }

        public virtual User User { get; set; }
    }
} 