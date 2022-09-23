using SmartMeterConsumerManagement.Models.DBContext;
using SmartMeterConsumerManagement.ServiceContracts;
using System;
using System.Linq;

namespace SmartMeterConsumerManagement.Repositories
{
    public class VendorRepository : IVendorRepository
    {
        private readonly SMCM_DBContext _dataContext;

        public VendorRepository(SMCM_DBContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void AddVendor(Vendor vendor)
        {
            _dataContext.Vendors.Add(vendor);
            _dataContext.SaveChanges();
        }

        public void UpdateVendor(Vendor vendor)
        {
            throw new NotImplementedException();
        }

        public Vendor GetVendorUsingContactNumber(string contactNumber)
        {
            return _dataContext.Vendors.FirstOrDefault(e => e.ContactNumber == contactNumber);
        }
    }
}
