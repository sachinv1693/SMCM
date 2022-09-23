using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using SmartMeterConsumerManagement.Models.DBContext;
using SmartMeterConsumerManagement.Repositories;

namespace SMCM_Testing.UnitTests
{
    public class VendorRepositoryTests
    {
        private static IConfiguration _config = ConfigManager.InitConfiguration();
        private static string _connectionString = _config.GetConnectionString("DevConnection");
        private readonly static DbContextOptions<SMCM_DBContext> _options = new DbContextOptionsBuilder<SMCM_DBContext>().UseSqlServer(_connectionString).Options;
        private readonly SMCM_DBContext _dBContext = new SMCM_DBContext(_options);
        private VendorRepository _vendorRepository;

        [SetUp]
        public void Setup()
        {
            _vendorRepository = new VendorRepository(_dBContext);
        }

        [Test]
        public void Test_Insert_Vendor()
        {
            Vendor vendor = new Vendor()
            {
                Name = "Viraj Electronics",
                Address = "104 Kamala Tower, Grant Road (East), Mumbai 400007",
                ContactNumber = "9090909090"
            };
            _vendorRepository.AddVendor(vendor);
            Assert.Pass();
        }

        [Test]
        public void Test_Get_Vendor_By_Contact_Number()
        {
            Vendor vendor = _vendorRepository.GetVendorUsingContactNumber("9090909090");
            Assert.AreEqual(2, vendor.Id);
        }
    }
}