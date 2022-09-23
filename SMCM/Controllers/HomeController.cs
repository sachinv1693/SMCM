using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using SmartMeterConsumerManagement.Controllers.ControllerUtils;
using SmartMeterConsumerManagement.Controllers.UserRequestHandler;
using SmartMeterConsumerManagement.Enums;
using SmartMeterConsumerManagement.Models;
using SmartMeterConsumerManagement.Models.DBContext;
using SmartMeterConsumerManagement.ServiceContracts;
using System;
using System.Collections.Generic;

namespace SmartMeterConsumerManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRequestRepository _userRequestRepository;
        private readonly ISmartMeterRepository _smartMeterRepository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IUserRepository userRepository,
            IUserRequestRepository userRequestRepository,
            ISmartMeterRepository smartMeterRepository,
            ILogger<HomeController> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
            _userRequestRepository = userRequestRepository;
            _smartMeterRepository = smartMeterRepository;
        }

        [Authorize] // Allow only signed in users to visit index page
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous] // Make About Page accesible to all
        public IActionResult About()
        {
            return View();
        }

        [Authorize] // Allow all authenticated users to sign out
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return Redirect("/authentication/login");
        }

        [Authorize] // Users with all roles should be able to edit their profile
        public IActionResult Edit()
        {
            UserClaimsHandler userClaims = new UserClaimsHandler(HttpContext?.User);
            string userEmailId = userClaims.GetUserEmailFromCurrentUserClaim();
            User user = _userRepository.GetUserByEmailAddress(userEmailId);
            var userLocation = _userRepository.GetUserLocationById(user.LocationId);
            UserModel userModel = new UserModel()
            {
                User = user,
                UserLocation = userLocation
            };
            return View(userModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(UserModel userModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _userRepository.UpdateUser(userModel.User);
                    _userRepository.UpdateUserLocation(userModel.UserLocation);
                    ViewData["EditedSuccessfully"] = true;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error in updating user profile: " + exception.Message);
                ViewData["EditedSuccessfully"] = false;
            }
            return View(userModel);
        }

        [Authorize(Roles = "GENERAL_USER, CONSUMER")]
        public IActionResult UserRequest()
        {
            DropdownSelectListBuilder listBuilder = new DropdownSelectListBuilder();
            ViewBag.RequestTypes = listBuilder.GetEnumTypeSelectList<UserRequestType>();
            return View();
        }

        [Authorize(Roles = "GENERAL_USER, CONSUMER")]
        [HttpPost]
        public IActionResult UserRequest(UserRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserClaimsHandler userClaims = new UserClaimsHandler(HttpContext?.User);
                    string userEmailId = userClaims.GetUserEmailFromCurrentUserClaim();
                    request.UserEmailId = userEmailId;
                    string userRole = _userRepository.GetUserRoleFromUserRequest(request);
                    if (request.Type.Trim() == UserRequestType.APPLY_FOR_SMART_METER_CONNECTION.ToString() && userRole.Trim() == "CONSUMER")
                    {
                        ViewData["FailureMessage"] = "You are a smart meter consumer. " +
                            "Do you wish to replace it? " +
                            "Please use (Raise Complaint) with (Faulty Smart Meter) option to file a complaint. Thanks.";
                        ViewData["RequestRaised"] = false;
                    }
                    else
                    {
                        RaiseRequest(request, ViewData);
                    }
                }
            }
            catch (Exception exception)
            {
                ViewData["RequestRaised"] = false;
                Console.WriteLine("Error while raising user request: " + exception.Message);
            }
            return UserRequest();
        }

        private void RaiseRequest(UserRequest request, ViewDataDictionary viewData)
        {
            bool hasAllSimilarRequestsGranted = _userRequestRepository.HasAllSimilarRequestsGranted(request.Type, request.UserEmailId);
            if (hasAllSimilarRequestsGranted)
            {
                request.Date = DateTime.Now;
                request.Status = UserRequestStatus.NOT_GRANTED.ToString();
                _userRequestRepository.AddUserRequest(request);
                viewData["RequestRaised"] = true;
            }
            else
            {
                viewData["FailureMessage"] = "Failed to raise your request. You might have already raised this request and it is yet to be approved. Feel free to contact your supervisor.";
                viewData["RequestRaised"] = false;
            }
        }

        [Authorize(Roles = "SUPERVISOR")]
        public IActionResult NewUserRequests()
        {
            List<UserRequest> requests = (List<UserRequest>)_userRequestRepository.GetAllUserRequests();
            ViewData["UserRequestData"] = (requests.Count > 0) ? requests : null;
            return View();
        }

        [Authorize(Roles = "SUPERVISOR")]
        [HttpPost]
        public IActionResult NewUserRequests(UserRequest request)
        {
            try
            {
                UserRequestFactory userRequestFactory = new UserRequestFactory(_userRepository, _smartMeterRepository, _userRequestRepository);
                Enum.TryParse(request.Type, out UserRequestType requestType);
                IUserRequestHandler userRequestHandler = userRequestFactory.GetUserRequestHandler(requestType);
                bool IsRequestHandled = userRequestHandler.HandleRequest(request, ViewData);
                ViewData["IsRequestApproved"] = IsRequestHandled;
            }
            catch (Exception exception)
            {
                ViewData["IsRequestApproved"] = false;
                ViewData["UnApprovalMessage"] = string.Empty;
                Console.WriteLine("Error in addressing new user request: " + exception.Message);
            }
            return NewUserRequests();
        }

        [Authorize(Roles = "GENERAL_USER, CONSUMER")]
        public IActionResult ViewRequests()
        {
            UserClaimsHandler userClaims = new UserClaimsHandler(HttpContext?.User);
            string userEmailId = userClaims.GetUserEmailFromCurrentUserClaim();
            List<UserRequest> requests = (List<UserRequest>)_userRequestRepository.GetAllUserRequestsByUserEmailId(userEmailId);
            ViewData["MyRequests"] = (requests.Count > 0) ? requests : null;
            return View();
        }

        [Authorize(Roles = "PROJECT_MANAGER")]
        public IActionResult DisplayUserDetails()
        {
            List<User> users = (List<User>)_userRepository.GetAllUsers();
            List<UserModel> userModels = new List<UserModel>();
            foreach (User user in users)
            {
                UserModel model = new UserModel
                {
                    User = user,
                    UserLocation = _userRepository.GetUserLocationById(user.LocationId)
                };
                userModels.Add(model);
            }
            ViewData["UsersData"] = (userModels.Count > 0) ? userModels : null;
            return View();
        }
    }
}
