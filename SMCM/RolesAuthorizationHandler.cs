using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using SmartMeterConsumerManagement.Models.DBContext;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartMeterConsumerManagement
{
    public class RolesAuthorizationHandler : AuthorizationHandler<RolesAuthorizationRequirement>, IAuthorizationHandler
    {
        private readonly SMCM_LoginContext _loginContext;
        public RolesAuthorizationHandler(SMCM_LoginContext loginContext)
        {
            _loginContext = loginContext;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       RolesAuthorizationRequirement requirement)
        {
            if (context.User == null || !context.User.Identity.IsAuthenticated)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var validRole = false;
            if (requirement.AllowedRoles == null ||
                requirement.AllowedRoles.Any() == false)
            {
                validRole = true;
            }
            else
            {
                var claims = context.User.Claims;
                var userEmail = claims.FirstOrDefault(c => c.Type == "UserEmail").Value;
                var roles = requirement.AllowedRoles;

                User user = _loginContext.GetUser(userEmail);
                validRole = roles.Contains(user.Role.Trim());
            }

            if (validRole)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}
