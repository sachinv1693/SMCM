using Microsoft.EntityFrameworkCore;
using SmartMeterConsumerManagement.Enums;
using SmartMeterConsumerManagement.Models.DBContext;
using SmartMeterConsumerManagement.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartMeterConsumerManagement.Repositories
{
    public class UserRequestRepository : IUserRequestRepository
    {
        private readonly SMCM_DBContext _dataContext;

        public UserRequestRepository(SMCM_DBContext dBContext)
        {
            _dataContext = dBContext;
        }

        public void AddUserRequest(UserRequest userRequest)
        {
            _dataContext.UserRequests.Add(userRequest);
            _dataContext.SaveChanges();
        }

        public void UpdateUserRequest(UserRequest userRequest, string consumerEmailId)
        {
            throw new NotImplementedException();
        }

        public void ApproveRequest(UserRequest request)
        {
            request.Status = UserRequestStatus.GRANTED.ToString();
            _dataContext.Entry(request).State = EntityState.Modified;
            _dataContext.SaveChanges();
        }

        public IList<UserRequest> GetAllUserRequestsByType(UserRequestType type)
        {
            return _dataContext.UserRequests.Where(e => e.Type == type.ToString()).ToList<UserRequest>();
        }

        public IList<UserRequest> GetAllUserRequestsByUserEmailId(string userEmailId)
        {
            return _dataContext.UserRequests.Where(e => e.UserEmailId == userEmailId).ToList<UserRequest>();
        }

        public IList<UserRequest> GetAllUserRequests()
        {
            return _dataContext.UserRequests.ToList<UserRequest>();
        }

        public IList<UserRequest> GetAllUserRequestsByDate(DateTime date)
        {
            return _dataContext.UserRequests.Where(e => e.Date.DayOfYear.CompareTo(date.DayOfYear) == 0).ToList<UserRequest>();
        }

        public IList<UserRequest> GetAllNeverGrantedUserRequests()
        {
            return _dataContext.UserRequests.Where(e => e.Status == UserRequestStatus.NOT_GRANTED.ToString()).ToList<UserRequest>();
        }

        public IList<UserRequest> GetAllGrantedUserRequests()
        {
            return _dataContext.UserRequests.Where(e => e.Status == UserRequestStatus.GRANTED.ToString()).ToList<UserRequest>();
        }

        public string GetUserRequestType(long userRequestId)
        {
            var UserRequest = _dataContext.UserRequests.FirstOrDefault(e => e.Id == userRequestId);
            return UserRequest.Type;
        }

        public void SetUserRequestStatus(long userRequestId, UserRequestStatus status)
        {
            (from p in _dataContext.UserRequests
             where p.Id == userRequestId
             select p).ToList().ForEach(x => x.Status = Convert.ToString(status));
            _dataContext.SaveChanges();
        }

        public string GetUserRequestStatus(long userRequestId)
        {
            var UserRequest = _dataContext.UserRequests.FirstOrDefault(e => e.Id == userRequestId);
            return UserRequest.Status;
        }

        public void SetUserRequestType(long userRequestId, UserRequestType type)
        {
            (from p in _dataContext.UserRequests
             where p.Id == userRequestId
             select p).ToList().ForEach(x => x.Type = Convert.ToString(type));
            _dataContext.SaveChanges();
        }

        public void RemoveUserRequestById(long userRequestId)
        {
            throw new NotImplementedException();
        }

        public bool HasAllSimilarRequestsGranted(string type, string userEmailId)
        {
            bool isNotFirstRequest = _dataContext.UserRequests.Any(x => x.Type == type && x.UserEmailId == userEmailId);
            return !isNotFirstRequest || _dataContext.UserRequests.All(x => x.UserEmailId == userEmailId && x.Type == type && x.Status == UserRequestStatus.GRANTED.ToString());
        }
    }
}
