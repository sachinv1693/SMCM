using System;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace SmartMeterConsumerManagement.Models.DBContext
{
    public partial class Complaint
    {
        [Display(Name = "Consumer Email")]
        public string ConsumerEmailId { get; set; }
        [Display(Name = "Complaint ID")]
        public long Id { get; set; }
        [Display(Name = "Complaint Date")]
        public DateTime Date { get; set; }
        [Required]
        [Display(Name = "Complaint Type")]
        public string Type { get; set; }
        [Display(Name = "Status")]
        public string Status { get; set; }
        [Required]
        [Display(Name = "Complaint Message")]
        public string Message { get; set; }

        public virtual User ConsumerEmail { get; set; }
    }
}
