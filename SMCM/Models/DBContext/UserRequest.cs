using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace SmartMeterConsumerManagement.Models.DBContext
{
    public partial class UserRequest
    {
        [Display(Name = "User Email")]
        public string UserEmailId { get; set; }
        [KeyAttribute()]
        [Display(Name = "Request ID")]
        public long Id { get; set; }
        [Display(Name = "Date")]
        public DateTime Date { get; set; }
        [Display(Name = "Request Type")]
        [Required]
        public string Type { get; set; }
        [Display(Name = "Status")]
        public string Status { get; set; }
        public virtual User ConsumerEmail { get; set; }
    }
}
