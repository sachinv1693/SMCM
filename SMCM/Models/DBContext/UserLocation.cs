using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace SmartMeterConsumerManagement.Models.DBContext
{
    public partial class UserLocation
    {
        public UserLocation()
        {
            Users = new HashSet<User>();
        }
        [Display(Name = "Location ID")]
        public string Id { get; set; }
        [Display(Name = "Area Code")]
        public long AreaCode { get; set; }
        [Required]
        [Display(Name = "Apartment Name")]
        public string ApartmentName { get; set; }
        [Required]
        [Display(Name = "Block No.")]
        public string BlockNumber { get; set; }
        [Required]
        [Display(Name = "Street")]
        public string Street { get; set; }
        [Required]
        [Display(Name = "City")]
        public string City { get; set; }
        [Required]
        [Display(Name = "State")]
        public string State { get; set; }
        [Required]
        [Display(Name = "Pincode")]
        public string Pincode { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
