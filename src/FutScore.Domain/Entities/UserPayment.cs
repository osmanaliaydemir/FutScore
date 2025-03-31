using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class UserPayment : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentType { get; set; }

        [Required]
        [StringLength(100)]
        public string TransactionId { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        [StringLength(10)]
        public string? Currency { get; set; }

        [StringLength(50)]
        public string? PaymentProvider { get; set; }
        [StringLength(50)]
        public string? PaymentMethod { get; set; }
        [StringLength(50)]
        public string? PaymentStatus { get; set; }

        [StringLength(200)]
        public string? BillingAddress { get; set; }
        [StringLength(100)]
        public string? BillingCity { get; set; }
        [StringLength(100)]
        public string? BillingCountry { get; set; }
        [StringLength(20)]
        public string? BillingPostalCode { get; set; }

        [StringLength(4)]
        public string? CardLastFourDigits { get; set; }
        [StringLength(50)]
        public string? CardBrand { get; set; }

        public DateTime PaymentDate { get; set; }
        public DateTime? RefundDate { get; set; }
        [StringLength(500)]
        public string? RefundReason { get; set; }

        // Navigation Properties
        public virtual User? User { get; set; }
    }
} 