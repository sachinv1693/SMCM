using Microsoft.EntityFrameworkCore;
using SmartMeterConsumerManagement.Enums;
using SmartMeterConsumerManagement.Models.DBContext;
using SmartMeterConsumerManagement.ServiceContracts;
using System.Collections.Generic;
using System.Linq;

namespace SmartMeterConsumerManagement.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SMCM_DBContext _dataContext;

        public UserRepository(SMCM_DBContext dBContext)
        {
            _dataContext = dBContext;
        }

        public IList<User> GetAllUsers()
        {
            var users = from user in _dataContext.Users
                         select user;
            return users.ToList();
        }

        public User GetUserById(long userId)
        {
            return _dataContext.Users.Where(s => s.Id == userId).FirstOrDefault();
        }

        public void AddUser(User user)
        {
            _dataContext.Users.Add(user);
            _dataContext.SaveChanges();
        }

        public void AddUserLocation(UserLocation userLocation)
        {
            _dataContext.UserLocations.Add(userLocation);
            _dataContext.SaveChanges();
        }

        public UserLocation GetUserLocationById(string locationId)
        {
            return _dataContext.UserLocations.Where(x => x.Id == locationId).FirstOrDefault();
        }

        public void UpdateUser(User user)
        {
            _dataContext.Entry(user).State = EntityState.Modified;
            _dataContext.Entry(user).Property(x => x.Id).IsModified = false;
            _dataContext.SaveChanges();
        }

        public void UpdateUserRole(UserRequest request, UserRole role)
        {
            var user = GetUserFromUserRequest(request);
            user.Role = role.ToString();
            _dataContext.SaveChanges();
        }

        public string GetUserRoleFromUserRequest(UserRequest request)
        {
            var user = GetUserFromUserRequest(request);
            return user.Role;
        }

        public void UpdateUserLocation(UserLocation location)
        {
            _dataContext.Entry(location).State = EntityState.Modified;
            _dataContext.Entry(location).Property(x => x.Id).IsModified = false;
            _dataContext.SaveChanges();
        }

        public User GetUserByEmailAddress(string emailId)
        {
            return _dataContext.Users.FirstOrDefault(e => e.EmailAddress == emailId);
        }

        public void AssignSmartMeter(string emailId, long smartMeterId)
        {
            (from u in _dataContext.Users
             where u.EmailAddress == emailId
             select u).ToList().ForEach(x => x.SmartMeterId = smartMeterId);
            _dataContext.SaveChanges();
        }

        public User GetUserByMobileNumber(string mobileNumber)
        {
            return _dataContext.Users.FirstOrDefault(e => e.MobileNumber == mobileNumber);
        }

        public User GetUserBySmartMeterId(long smartMeterId)
        {
            return _dataContext.Users.FirstOrDefault(e => e.SmartMeterId == smartMeterId);
        }

        public IList<User> GetAllUsersByAreaCode(long areaCode)
        {
            var areaWiseUsers = (from user in _dataContext.Users
                                join userLocation in _dataContext.UserLocations on user.LocationId equals userLocation.Id
                                where userLocation.AreaCode == areaCode
                                orderby user.EmailAddress ascending
                                select user).ToList();
            return areaWiseUsers;
        }

        public void DeleteUserByEmailAddress(string emailId)
        {
            var user = _dataContext.Users.Where(x => x.EmailAddress == emailId).First();
            _dataContext.Users.Remove(user);
            _dataContext.SaveChanges();
        }

        public User GetUserFromUserRequest(UserRequest request)
        {
            User user = _dataContext.Users.Where(x => x.EmailAddress == request.UserEmailId).FirstOrDefault();
            return user;
        }

        public void UpdateUserRole(string emailId, UserRole role)
        {
            var user = _dataContext.Users.First(u => u.EmailAddress == emailId);
            user.Role = role.ToString();
            _dataContext.SaveChanges();
        }
    }
}
