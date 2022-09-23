using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace SmartMeterConsumerManagement.Models.DBContext
{
    public partial class Bill
    {
        [Display(Name = "Bill ID")]
        public long Id { get; set; }
        [Display(Name = "Consumer Email ID")]
        public string ConsumerEmailId { get; set; }
        [Display(Name = "Bill Date")]
        public DateTime Date { get; set; }
        [Display(Name = "Smart Meter ID")]
        public long? SmartMeterId { get; set; }
        [Display(Name = "Current Reading Unit")]
        public double? CurrentReadingUnit { get; set; }
        [Display(Name = "Previous Reading Unit")]
        public double? PreviousReadingUnit { get; set; }
        [Display(Name = "Current Bill Amount")]
        public double? CurrentBillingAmount { get; set; }
        [Display(Name = "Previous Bill Amount")]
        public double? PreviousBillingAmount { get; set; }
        [Display(Name = "Billing Month")]
        public string CurrentBillingMonth { get; set; }
        [Display(Name = "Payment Status")]
        public string PaymentStatus { get; set; }
        [Display(Name = "Payment Type")]
        public string PaymentType { get; set; }
        [Display(Name = "Payment Date")]
        public DateTime? PaymentDate { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string PaymentState { get; set; }

        public virtual User ConsumerEmail { get; set; }
    }
}
