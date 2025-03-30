using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class UserSubscription : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string PlanType { get; set; }

        [Required]
        [StringLength(100)]
        public string PlanName { get; set; }

        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsAutoRenewal { get; set; }

        public string PaymentProvider { get; set; }
        public string SubscriptionId { get; set; }
        public string CustomerId { get; set; }

        // Navigation Properties
        public virtual User User { get; set; }
        public virtual ICollection<UserPayment> Payments { get; set; }
    }
} 