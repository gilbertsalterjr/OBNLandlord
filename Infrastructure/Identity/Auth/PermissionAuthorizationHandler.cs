using Application.Features.Identity.Users;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Auth
{
    internal class PermissionAuthorizationHandler(IUserService userService) : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IUserService _userService = userService;
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User.GetUserId() is { } userId &&
                await _userService.IsPermissionAssignedAsync(userId, requirement.Permission))
            {
                context.Succeed(requirement);
            }
        }
    }
}
