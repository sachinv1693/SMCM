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
    public class ComplaintRepositoryTests
    {
        private static IConfiguration _config = ConfigManager.InitConfiguration();
        private static string _connectionString = _config.GetConnectionString("DevConnection");
        private readonly static DbContextOptions<SMCM_DBContext> _options = new DbContextOptionsBuilder<SMCM_DBContext>().UseSqlServer(_connectionString).Options;
        private readonly SMCM_DBContext _dBContext = new SMCM_DBContext(_options);
        private ComplaintRepository _complaintRepository;

        [SetUp]
        public void Setup()
        {
            _complaintRepository = new ComplaintRepository(_dBContext);
        }

        [Test]
        public void Test_Insert_Complaint()
        {
            Complaint complaint = new Complaint()
            {
               ConsumerEmailId = "abc@gmail.com",
               Date = DateTime.Now,
               Type = ComplaintType.METER_HIGH_BILL.ToString(),
               Status = ComplaintStatus.PENDING.ToString(),
               Message = "I have got very high bill amount for the month of May 2021 than usual. Please check in your system and revert. Thanks. - Mr ABC, BTM, Bangalore"
            };
            _complaintRepository.AddComplaint(complaint);
            Assert.Pass();
        }

        [Test]
        public void Test_Get_All_Complaints_By_Type()
        {
            IList<Complaint> complaints = _complaintRepository.GetAllComplaintsByType(ComplaintType.METER_HIGH_BILL);
            Assert.AreEqual(0, complaints.Count);
        }

        [Test]
        public void Test_Get_Complaints_By_Consumer_Email_Id()
        {
            IList<Complaint> complaints = _complaintRepository.GetAllComplaintsByConsumerEmailId("abc@gmail.com");
            Assert.AreEqual(1, complaints.Count);
        }

        [Test]
        public void Test_Get_All_Complaints_By_Date()
        {
            DateTime date = new DateTime(2021, 6, 11);
            IList<Complaint> complaints = _complaintRepository.GetAllComplaintsByDate(date);
            Assert.AreEqual(1, complaints.Count);
        }

        [Test]
        public void Test_Get_All_Unresolved_Complaints()
        {
            IList<Complaint> complaints = _complaintRepository.GetAllUnresolvedComplaints();
            Assert.AreEqual(1, complaints.Count);
        }

        [Test]
        public void Test_Get_All_Resolved_Complaints()
        {
            IList<Complaint> complaints = _complaintRepository.GetAllResolvedComplaints();
            Assert.AreEqual(0, complaints.Count);
        }

        [Test]
        public void Test_Get_Complaint_Type()
        {
            string complaintType = _complaintRepository.GetComplaintType(1);
            Assert.AreEqual(ComplaintType.METER_HIGH_BILL.ToString(), complaintType);
        }

        [Test]
        public void Test_Set_Complaint_Status()
        {
            _complaintRepository.SetComplaintStatus(1, ComplaintStatus.IN_REVIEW);
            Assert.Pass();
        }

        [Test]
        public void Test_Get_Complaint_Status()
        {
            string complaintStatus = _complaintRepository.GetComplaintStatus(1);
            Assert.AreEqual(ComplaintStatus.IN_REVIEW.ToString(), complaintStatus);
        }

        [Test]
        public void Test_Set_Complaint_Type()
        {
            _complaintRepository.SetComplaintType(1, ComplaintType.LAST_METER_READING);
            Assert.Pass();
        }
    }
}