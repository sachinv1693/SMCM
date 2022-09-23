using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmartMeterConsumerManagement.Controllers.ControllerUtils
{
    public class UserClaimsHandler : Controller
    {
        private readonly ClaimsPrincipal _userClaim;
        public UserClaimsHandler(ClaimsPrincipal userClaim)
        {
            _userClaim = userClaim;
        }

        public string GetUserEmailFromCurrentUserClaim()
        {
            var claims = _userClaim.Identities.First().Claims.ToList();
            // Filter email Id claim
            var userEmailId = claims?.FirstOrDefault(x => x.Type.Equals("UserEmail", StringComparison.OrdinalIgnoreCase))?.Value;
            return userEmailId;
        }
    }
}
