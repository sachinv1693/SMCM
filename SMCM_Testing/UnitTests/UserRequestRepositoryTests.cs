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
    public class UserRequestRepositoryTests
    {
        private static IConfiguration _config = ConfigManager.InitConfiguration();
        private static string _connectionString = _config.GetConnectionString("DevConnection");
        private readonly static DbContextOptions<SMCM_DBContext> _options = new DbContextOptionsBuilder<SMCM_DBContext>().UseSqlServer(_connectionString).Options;
        private readonly SMCM_DBContext _dBContext = new SMCM_DBContext(_options);
        private UserRequestRepository _userRequestRepository;

        [SetUp]
        public void Setup()
        {
            _userRequestRepository = new UserRequestRepository(_dBContext);
        }

        [Test]
        public void Test_Insert_User_Request()
        {
            UserRequest request = new UserRequest()
            {
               UserEmailId = "shyam@gmail.com",
               Date = DateTime.Now,
               Type = UserRequestType.APPLY_FOR_SMART_METER_CONNECTION.ToString(),
               Status = UserRequestStatus.NOT_GRANTED.ToString(),
            };
            _userRequestRepository.AddUserRequest(request);
            Assert.Pass();
        }

        [Test]
        public void Test_Get_All_User_Requests_By_Type()
        {
            IList<UserRequest> requests = _userRequestRepository.GetAllUserRequestsByType(UserRequestType.APPLY_FOR_SMART_METER_CONNECTION);
            Assert.AreEqual(1, requests.Count);
        }

        [Test]
        public void Test_Get_User_Requests_By_User_Email_Id()
        {
            IList<UserRequest> requests = _userRequestRepository.GetAllUserRequestsByUserEmailId("neeti@gmail.com");
            Assert.AreEqual(1, requests.Count);
        }

        [Test]
        public void Test_Get_All_User_Requests_By_Date()
        {
            DateTime date = new DateTime(2021, 6, 25);
            IList<UserRequest> requests = _userRequestRepository.GetAllUserRequestsByDate(date);
            Assert.AreEqual(1, requests.Count);
        }

        [Test]
        public void Test_Get_All_Never_Granted_User_Requests()
        {
            IList<UserRequest> complaints = _userRequestRepository.GetAllNeverGrantedUserRequests();
            Assert.AreEqual(1, complaints.Count);
        }

        [Test]
        public void Test_Get_All_Granted_User_Requests()
        {
            IList<UserRequest> requests = _userRequestRepository.GetAllGrantedUserRequests();
            Assert.AreEqual(0, requests.Count);
        }

        [Test]
        public void Test_Get_User_Request_Type()
        {
            string requestType = _userRequestRepository.GetUserRequestType(1);
            Assert.AreEqual(UserRequestType.APPLY_FOR_SMART_METER_CONNECTION.ToString(), requestType.Trim());
        }

        [Test]
        public void Test_Set_User_Request_Status()
        {
            _userRequestRepository.SetUserRequestStatus(1, UserRequestStatus.GRANTED);
            Assert.Pass();
        }

        [Test]
        public void Test_Get_User_Request_Status()
        {
            string requestStatus = _userRequestRepository.GetUserRequestStatus(3);
            Assert.AreEqual(UserRequestStatus.GRANTED.ToString(), requestStatus.Trim());
        }

        [Test]
        public void Test_Set_User_Request_Type()
        {
            _userRequestRepository.SetUserRequestType(1, UserRequestType.CHANGE_OF_ADDRESS);
            Assert.Pass();
        }
    }
}