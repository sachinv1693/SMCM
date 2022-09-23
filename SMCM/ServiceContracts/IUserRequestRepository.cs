using SmartMeterConsumerManagement.Enums;
using SmartMeterConsumerManagement.Models.DBContext;
using System;
using System.Collections.Generic;

namespace SmartMeterConsumerManagement.ServiceContracts
{
    public interface IUserRequestRepository
    {
        void AddUserRequest(UserRequest userRequest);
        void ApproveRequest(UserRequest userRequest);
        IList<UserRequest> GetAllUserRequests();
        IList<UserRequest> GetAllGrantedUserRequests();
        IList<UserRequest> GetAllNeverGrantedUserRequests();
        IList<UserRequest> GetAllUserRequestsByUserEmailId(string userEmailId);
        IList<UserRequest> GetAllUserRequestsByDate(DateTime date);
        IList<UserRequest> GetAllUserRequestsByType(UserRequestType type);
        string GetUserRequestStatus(long userRequestId);
        string GetUserRequestType(long userRequestId);
        void RemoveUserRequestById(long UserRequestId);
        void SetUserRequestStatus(long UserRequestId, UserRequestStatus status);
        void SetUserRequestType(long UserRequestId, UserRequestType type);
        void UpdateUserRequest(UserRequest userRequest, string userEmailId);
        bool HasAllSimilarRequestsGranted(string type, string userEmailId);
    }
}