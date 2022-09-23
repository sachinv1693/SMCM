using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SmartMeterConsumerManagement.Enums;
using SmartMeterConsumerManagement.Models.DBContext;
using SmartMeterConsumerManagement.ServiceContracts;
using System.Collections.Generic;

namespace SmartMeterConsumerManagement.Controllers.UserRequestHandler
{
    public class SmartMeterRequestHandler : IUserRequestHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly ISmartMeterRepository _smartMeterRepository;
        private readonly IUserRequestRepository _userRequestRepository;

        public SmartMeterRequestHandler(IUserRepository userRepository,
            ISmartMeterRepository smartMeterRepository,
            IUserRequestRepository userRequestRepository)
        {
            _userRepository = userRepository;
            _smartMeterRepository = smartMeterRepository;
            _userRequestRepository = userRequestRepository;
        }

        public bool HandleRequest(UserRequest request, ViewDataDictionary viewData)
        {
            List<SmartMeter> availableMeters = (List<SmartMeter>)_smartMeterRepository.GetUnusedSmartMeters();
            if (availableMeters.Count > 0)
            {
                SmartMeter meterToBeAssigned = availableMeters[0];
                _smartMeterRepository.UpdateSmartMeterStatus(meterToBeAssigned, SmartMeterStatus.CONNECTED);
                _userRepository.UpdateUserRole(request, UserRole.CONSUMER);
                _userRepository.AssignSmartMeter(request.UserEmailId, meterToBeAssigned.Id);
                _userRequestRepository.ApproveRequest(request);
                return true;
            }
            else
            {
                viewData["UnApprovalMessage"] = "Smart meter may not be available at the moment.";
                return false;
            }
        }
    }
}