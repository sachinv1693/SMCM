using SmartMeterConsumerManagement.Enums;
using SmartMeterConsumerManagement.Models.DBContext;
using System.Collections.Generic;

namespace SmartMeterConsumerManagement.ServiceContracts
{
    public interface ISmartMeterRepository
    {
        void AddSmartMeter(SmartMeter meter);
        void UpdateSmartMeter(SmartMeter meter);
        string GetSmartMeterStatus(long smartMeterId);
        IList<SmartMeter> GetUnusedSmartMeters();
        SmartMeter GetSmartMeterWithId(long smartMeterId);
        void UpdateSmartMeterStatus(SmartMeter smartMeter, SmartMeterStatus status);
    }
}
