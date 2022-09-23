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
    public class UserRepositoryTests
    {
        private static IConfiguration _config = ConfigManager.InitConfiguration();
        private static string _connectionString = _config.GetConnectionString("DevConnection");
        private readonly static DbContextOptions<SMCM_DBContext> _options = new DbContextOptionsBuilder<SMCM_DBContext>().UseSqlServer(_connectionString).Options;
        private readonly SMCM_DBContext _dBContext = new SMCM_DBContext(_options);
        private UserRepository _userRepository;

        [SetUp]
        public void Setup()
        {
            _userRepository = new UserRepository(_dBContext);
        }

        [Test]
        public void Test_Insert_User()
        {
            User user = new User()
            {
                Role = UserRole.GENERAL_USER.ToString(),
                EmailAddress = "mohan@gmail.com",
                Password = "mohan",
                FirstName = "Mohan",
                LastName = "Sinha",
                LocationId = "dc94d7c6-c479-46fa-adbd-a079705dafzz",
                MobileNumber = "9898989696",
                LanguageSelected = "English",
                CreatedAt = DateTime.Now
            };
            _userRepository.AddUser(user);
            Assert.Pass();
        }

        [Test]
        public void Test_Insert_UserLocation()
        {
            UserLocation userLocation = new UserLocation()
            {
                Id = "dc94d7c6-c479-46fa-adbd-a079705dafzz",
                AreaCode = 440,
                ApartmentName = "Sri Laxmi Apartment",
                Street = "Nagarpada street",
                BlockNumber = "204",
                City = "Bangalore",
                State = "Karnataka",
                Pincode = "560035"
            };
            _userRepository.AddUserLocation(userLocation);
            Assert.Pass();
        }

        [Test]
        public void Test_Get_All_Users()
        {
            List<User> users = (List<User>)_userRepository.GetAllUsers();
            Assert.AreEqual(3, users.Count);
        }

        [Test]
        public void Test_Get_User_By_Email_Id()
        {
            User user = _userRepository.GetUserByEmailAddress("ram@gmail.com");
            Assert.AreEqual(20026, user.Id);
        }

        [Test]
        public void Test_Get_User_By_Mobile_Number()
        {
            User user = _userRepository.GetUserByMobileNumber("6666666668");
            Assert.AreEqual(20026, user.Id);
        }

        [Test]
        public void Test_Assign_Smart_Meter()
        {
            string emailAddress = "ram@gmail.com";
            long smartMeterId = 2;
            _userRepository.AssignSmartMeter(emailAddress, smartMeterId);
            Assert.Pass();
        }

        [Test]
        public void Test_Get_All_Users_Per_Area_Code()
        {
            long areaCode = 400;
            List<User> userList = (List<User>)_userRepository.GetAllUsersByAreaCode(areaCode);
            Assert.AreEqual(2, userList.Count);
        }

        [Test]
        public void Test_Updating_User_Role()
        {
            string userEmail = "ram@gmail.com";
            _userRepository.UpdateUserRole(userEmail, UserRole.CONSUMER);
            Assert.Pass();
        }
    }
}