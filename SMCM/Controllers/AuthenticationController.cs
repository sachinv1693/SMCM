using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartMeterConsumerManagement.Controllers.ControllerUtils;
using SmartMeterConsumerManagement.Enums;
using SmartMeterConsumerManagement.Models;
using SmartMeterConsumerManagement.Models.DBContext;
using SmartMeterConsumerManagement.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmartMeterConsumerManagement.Controllers
{
    [AllowAnonymous]
    [Authorize(AuthenticationSchemes = Startup.CookieScheme)]
    public class AuthenticationController : Controller
    {
        private readonly SMCM_LoginContext _loginContext;
        private readonly IUserRepository _userRepository;

        public AuthenticationController(SMCM_LoginContext loginContext, IUserRepository userRepository)
        {
            _loginContext = loginContext;
            _userRepository = userRepository;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            DropdownSelectListBuilder listBuilder = new DropdownSelectListBuilder();
            ViewBag.UserCategories = listBuilder.GetEnumTypeSelectList<UserCategory>();
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(UserModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.UserLocation.Id = model.User.LocationId = Guid.NewGuid().ToString();
                    _userRepository.AddUserLocation(model.UserLocation);
                    model.User.CreatedAt = DateTime.Now;
                    model.User.Role = UserRole.GENERAL_USER.ToString();
                    _userRepository.AddUser(model.User);
                    ViewData["IsNewUserAdded"] = true;
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Credentials");
            }
            catch (Exception exception)
            {
                ViewData["IsNewUserAdded"] = false;
                Console.WriteLine("Error while creating the user account: " + exception.Message);
            }
            return SignUp();
        }
        
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = _loginContext.GetUser(model.EmailAddress);
                    if (ValidateUser(user, model))
                    {
                        string userRole = user.Role.ToString().Trim();
                        var claims = GetClaims(userRole, model);
                        await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "User Identity")));
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Credentials");
            }
            catch (Exception exception)
            {
                Console.WriteLine("Login Error: " + exception.Message);
                ModelState.AddModelError(string.Empty, "Login Error!! Entry could be invalid. Please contact the supervisor.");
            }
            return View();
        }

        private IEnumerable<Claim> GetClaims(string userRole, LoginModel model)
        {
            var claims = new List<Claim>
            {
                new Claim("UserEmail", model.EmailAddress),
                new Claim("UserRole", userRole)
            };
            return claims;
        }

        public IActionResult About()
        {
            return RedirectToAction("/home/about");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return Redirect("Login");
        }

        private bool ValidateUser(User user, LoginModel model)
        {
            return (user?.Id > 0 && user.EmailAddress.ToString().Trim() == model.EmailAddress && user.Password.ToString().Trim() == model.Password);
        }
    }
}
