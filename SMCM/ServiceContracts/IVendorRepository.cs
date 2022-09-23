using SmartMeterConsumerManagement.Models.DBContext;

namespace SmartMeterConsumerManagement.ServiceContracts
{
    public interface IVendorRepository
    {
        void AddVendor(Vendor vendor);
        void UpdateVendor(Vendor vendor);
        Vendor GetVendorUsingContactNumber(string contactNumber);
    }
}
