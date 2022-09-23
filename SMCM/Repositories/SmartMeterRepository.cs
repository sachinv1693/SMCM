using SmartMeterConsumerManagement.Enums;
using SmartMeterConsumerManagement.Models.DBContext;
using SmartMeterConsumerManagement.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartMeterConsumerManagement.Repositories
{
    public class SmartMeterRepository : ISmartMeterRepository
    {

        private readonly SMCM_DBContext _dataContext;

        public SmartMeterRepository(SMCM_DBContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void AddSmartMeter(SmartMeter meter)
        {
            _dataContext.SmartMeters.Add(meter);
            _dataContext.SaveChanges();
        }

        public void UpdateSmartMeter(SmartMeter meter)
        {
            throw new NotImplementedException();
        }

        public string GetSmartMeterStatus(long smartMeterId)
        {
            var meter = _dataContext.SmartMeters.FirstOrDefault(e => e.Id == smartMeterId);
            return meter.Status;
        }

        public IList<SmartMeter> GetUnusedSmartMeters()
        {
            return _dataContext.SmartMeters.Where(x => x.Status == SmartMeterStatus.UNUSED.ToString()).ToList<SmartMeter>();
        }

        public SmartMeter GetSmartMeterWithId(long smartMeterId)
        {
            return _dataContext.SmartMeters.FirstOrDefault(e => e.Id == smartMeterId);
        }

        public void UpdateSmartMeterStatus(SmartMeter smartMeter, SmartMeterStatus status)
        {
            smartMeter.Status = status.ToString();
            _dataContext.SaveChanges();
        }        
    }
}
