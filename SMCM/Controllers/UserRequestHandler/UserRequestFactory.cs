using SmartMeterConsumerManagement.ServiceContracts;
using SmartMeterConsumerManagement.Enums;

namespace SmartMeterConsumerManagement.Controllers.UserRequestHandler
{
    public class UserRequestFactory
    {
        private readonly IUserRepository UserRepository;
        private readonly ISmartMeterRepository SmartMeterRepository;
        private readonly IUserRequestRepository UserRequestRepository;

        public UserRequestFactory(IUserRepository userRepository,
            ISmartMeterRepository smartMeterRepository,
            IUserRequestRepository userRequestRepository)
        {
            UserRepository = userRepository;
            SmartMeterRepository = smartMeterRepository;
            UserRequestRepository = userRequestRepository;
        }

        public IUserRequestHandler GetUserRequestHandler(UserRequestType requestType)
        {
            return requestType switch
            {
                UserRequestType.APPLY_FOR_SMART_METER_CONNECTION => new SmartMeterRequestHandler(UserRepository, SmartMeterRepository, UserRequestRepository),
                UserRequestType.CHANGE_OF_ADDRESS => new ChangeOfAddressRequestHandler(UserRepository, SmartMeterRepository, UserRequestRepository),
                UserRequestType.CLOSE_ACCOUNT => new CloseAccountRequestHandler(UserRepository, SmartMeterRepository, UserRequestRepository),
                _ => null
            };
        }
    }
}
