using System;
using System.Collections.Generic;

#nullable disable

namespace SmartMeterConsumerManagement.Models.DBContext
{
    public partial class SmartMeter
    {
        public long Id { get; set; }
        public string Status { get; set; }
        public double? CurrentMonthReading { get; set; }
        public int? VendorId { get; set; }
        public DateTime? PurchaseDate { get; set; }

        public virtual Vendor Vendor { get; set; }
    }
}
