using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FutScore.Domain.Entities
{
    public class Friendship : BaseEntity
    {
        [Required]
        public Guid RequesterId { get; set; }

        [Required]
        public Guid AddresseeId { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; }

        public DateTime? AcceptedAt { get; set; }
        public DateTime? RejectedAt { get; set; }
        public DateTime? UnfriendedAt { get; set; }

        // Navigation Properties
        [ForeignKey("RequesterId")]
        public virtual User? Requester { get; set; }

        [ForeignKey("AddresseeId")]
        public virtual User? Addressee { get; set; }
    }
} 