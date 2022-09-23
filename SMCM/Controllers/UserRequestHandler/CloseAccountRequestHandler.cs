using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SmartMeterConsumerManagement.Models.DBContext;
using SmartMeterConsumerManagement.ServiceContracts;

namespace SmartMeterConsumerManagement.Controllers.UserRequestHandler
{
    public class CloseAccountRequestHandler : IUserRequestHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly ISmartMeterRepository _smartMeterRepository;
        private readonly IUserRequestRepository _userRequestRepository;

        public CloseAccountRequestHandler(IUserRepository userRepository,
            ISmartMeterRepository smartMeterRepository,
            IUserRequestRepository userRequestRepository)
        {
            _userRepository = userRepository;
            _smartMeterRepository = smartMeterRepository;
            _userRequestRepository = userRequestRepository;
        }

        public bool HandleRequest(UserRequest request, ViewDataDictionary viewData)
        {
            throw new System.NotImplementedException();
        }
    }
}