using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace SmartMeterConsumerManagement.Models.DBContext
{
    public partial class User
    {
        public User()
        {
            Bills = new HashSet<Bill>();
            Complaints = new HashSet<Complaint>();
        }

        public long Id { get; set; }
        public string Role { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name= "Email Address")] 
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }
        [Display(Name = "Language")]
        public string LanguageSelected { get; set; }
        [HiddenInput(DisplayValue = false)]
        public bool HasLoggedIn { get; set; }
        [HiddenInput(DisplayValue = false)]
        public DateTime CreatedAt { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int? SessionTimerCount { get; set; }
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Smart Meter Id")]
        public long? SmartMeterId { get; set; }
        public string LocationId { get; set; }
        [Required]
        [Display(Name = "User Category")]
        public string UserCategory { get; set; }
        public virtual UserLocation Location { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<Complaint> Complaints { get; set; }
        public virtual ICollection<UserRequest> UserRequests { get; set; }
    }
}
