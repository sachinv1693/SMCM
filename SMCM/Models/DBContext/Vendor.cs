using System.Collections.Generic;

#nullable disable

namespace SmartMeterConsumerManagement.Models.DBContext
{
    public partial class Vendor
    {
        public Vendor()
        {
            SmartMeters = new HashSet<SmartMeter>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }

        public virtual ICollection<SmartMeter> SmartMeters { get; set; }
    }
}
