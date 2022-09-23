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
    public class BillRepositoryTests
    {
        private static IConfiguration _config = ConfigManager.InitConfiguration();
        private static string _connectionString = _config.GetConnectionString("DevConnection");
        private readonly static DbContextOptions<SMCM_DBContext> _options = new DbContextOptionsBuilder<SMCM_DBContext>().UseSqlServer(_connectionString).Options;
        private readonly SMCM_DBContext _dBContext = new SMCM_DBContext(_options);
        private BillRepository _billRepository;

        [SetUp]
        public void Setup()
        {
            _billRepository = new BillRepository(_dBContext);
        }

        [Test]
        public void Test_Insert_Bill()
        {
            Bill bill = new Bill()
            {
                ConsumerEmailId = "neeti@gmail.com",
                Date = DateTime.Now,
                SmartMeterId = 3,
                CurrentReadingUnit = 30.0,
                CurrentBillingAmount = 300.0,
                PreviousReadingUnit = 0.0,
                PreviousBillingAmount = 0.0,
                CurrentBillingMonth = DateTime.Now.ToString("MMM").ToUpper(),
                PaymentStatus = PaymentStatus.NOT_PAID.ToString()
            };
            _billRepository.AddBill(bill);
            Assert.Pass();
        }

        [Test]
        public void Test_Get_Bill_By_Id()
        {
            Bill bill = _billRepository.GetBillById(3);
            Assert.AreEqual(bill.CurrentBillingAmount, 300.0);
        }

        [Test]
        public void Test_Get_Bills_By_consumer_Email_Id()
        {
            IList<Bill> bills = _billRepository.GetBillsByConsumerEmailId("ram@gmail.com");
            Assert.AreEqual(2, bills.Count);
        }

        [Test]
        public void Test_Get_Monthly_Bill()
        {
            var previousMonth = DateTime.Now.AddMonths(-1).ToString("MMM").ToUpper(); // Current Month = 0, Previous Month = -1
            Enum.TryParse(previousMonth, out Month lastMonth); // Extract Month Enumerator string
            Bill bill = _billRepository.GetMonthWiseBill(3, lastMonth);
            Assert.AreEqual("neeti@gmail.com", bill.ConsumerEmailId.Trim());
        }

        [Test]
        public void Test_Get_Consumer_Email_Id_By_SmartMeterId()
        {
            var consumer = _billRepository.GetConsumerBySmartMeterId(3);
            Assert.AreEqual("neeti@gmail.com", consumer.EmailAddress.Trim());
        }

        [Test]
        public void Test_Get_Bills_By_Consumer_Email_Id()
        {
            var bills = _billRepository.GetBillsByConsumerEmailId("ram@gmail.com");
            Assert.AreEqual(2, bills.Count);
        }
    }
}