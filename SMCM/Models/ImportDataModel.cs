using SmartMeterConsumerManagement.Enums;
using System.ComponentModel.DataAnnotations;

namespace SmartMeterConsumerManagement.Models
{
    public class ImportDataModel
    {
        [Required]
        public ImportType ImportDataType { get; set; }
        public dynamic Data { get; set; }
    }
}
