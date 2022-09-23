using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using SmartMeterConsumerManagement.Enums;
using SmartMeterConsumerManagement.Models.DBContext;
using SmartMeterConsumerManagement.Repositories;
using System;
using System.Collections.Generic;

namespace SMCM_Testing.UnitTests
{
    public class SmartMeterRepositoryTests
    {
        private static IConfiguration _config = ConfigManager.InitConfiguration();
        private static string _connectionString = _config.GetConnectionString("DevConnection");
        private readonly static DbContextOptions<SMCM_DBContext> _options = new DbContextOptionsBuilder<SMCM_DBContext>().UseSqlServer(_connectionString).Options;
        private readonly SMCM_DBContext _dBContext = new SMCM_DBContext(_options);
        private SmartMeterRepository _smartMeterRepository;

        [SetUp]
        public void Setup()
        {
            _smartMeterRepository = new SmartMeterRepository(_dBContext);
        }

        [Test]
        public void Test_Insert_Smart_Meter()
        {
            SmartMeter meter = new SmartMeter()
            {
                Status = SmartMeterStatus.UNUSED.ToString(),
                CurrentMonthReading = 0,
                VendorId = 1,
                PurchaseDate = new DateTime(2022, 2, 16)
            };
            _smartMeterRepository.AddSmartMeter(meter);
            Assert.Pass();
        }

        [Test]
        public void Test_Get_Smart_Meter_By_Id()
        {
            SmartMeter meter = _smartMeterRepository.GetSmartMeterWithId(3);
            Assert.AreEqual(3, meter.Id);
        }

        [Test]
        public void Test_Get_Unused_Smart_Meters()
        {
            IList<SmartMeter> meters = _smartMeterRepository.GetUnusedSmartMeters();
            Assert.AreEqual(4, meters.Count);
        }
    }
}