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

        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }

        public string PaymentProvider { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }

        public string BillingAddress { get; set; }
        public string BillingCity { get; set; }
        public string BillingCountry { get; set; }
        public string BillingPostalCode { get; set; }

        public string CardLastFourDigits { get; set; }
        public string CardBrand { get; set; }

        public DateTime PaymentDate { get; set; }
        public DateTime? RefundDate { get; set; }
        public string RefundReason { get; set; }

        // Navigation Properties
        public virtual User User { get; set; }
    }
} 