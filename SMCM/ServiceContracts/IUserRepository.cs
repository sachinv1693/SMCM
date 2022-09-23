using SmartMeterConsumerManagement.Enums;
using SmartMeterConsumerManagement.Models.DBContext;
using System.Collections.Generic;

namespace SmartMeterConsumerManagement.ServiceContracts
{
    public interface IUserRepository
    {
        IList<User> GetAllUsers();
        User GetUserById(long userId);
        void AddUser(User user);
        void AddUserLocation(UserLocation userLocation);
        void UpdateUser(User user);
        void UpdateUserRole(UserRequest request, UserRole role);
        string GetUserRoleFromUserRequest(UserRequest request);
        void UpdateUserLocation(UserLocation location);
        UserLocation GetUserLocationById(string locationId);
        User GetUserByEmailAddress(string emailId);
        User GetUserByMobileNumber(string mobileNumber);
        User GetUserBySmartMeterId(long smartMeterId);
        IList<User> GetAllUsersByAreaCode(long areaCode);
        void AssignSmartMeter(string emailId, long smartMeterId);
        void DeleteUserByEmailAddress(string emailId);
    }
}
