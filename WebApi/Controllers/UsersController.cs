using Application.Features.Identity.Users;
using Application.Features.Identity.Users.Commands;
using Application.Features.Identity.Users.Queries;
using Infrastructure.Identity.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Infrastructure.Identity.Constants;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : BaseApiController
    {
        [HttpPost("register")]
        //[AllowAnonymous]
        [ShouldHavePermission(OBNTenantAction.Create, OBNTenantFeature.Users)]
        public async Task<IActionResult> RegisterUserAsync([FromBody] CreateUserRequest createUserRequest)
        {
            var response = await Sender.Send(new CreateUserCommand { CreateUserRequest = createUserRequest });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPut("update")]
        [ShouldHavePermission(OBNTenantAction.Update, OBNTenantFeature.Users)]
        public async Task<IActionResult> UpdateUserDetailsAsync([FromBody] UpdateUserRequest updateUser)
        {
            var response = await Sender.Send(new UpdateUserCommand { UpdateUserRequest = updateUser });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpPut("update-status")]
        [ShouldHavePermission(OBNTenantAction.Update, OBNTenantFeature.Users)]
        public async Task<IActionResult> ChangeUserStatusAsync([FromBody] ChangeUserStatusRequest changeUserStatus)
        {
            var response = await Sender.Send(new UpdateUserStatusCommand { ChangeUserStatus = changeUserStatus });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpPut("update-roles/{roleId}")]
        [ShouldHavePermission(OBNTenantAction.Update, OBNTenantFeature.UserRoles)]
        public async Task<IActionResult> UpdateUserRolesAsync([FromBody] UserRolesRequest userRolesRequest, string roleId)
        {
            var response = await Sender.Send(new UpdateUserRolesCommand {UserRolesRequest = userRolesRequest, RoleId = roleId });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpDelete("delete/{userId}")]
        [ShouldHavePermission(OBNTenantAction.Delete, OBNTenantFeature.Users)]
        public async Task<IActionResult> DeleteUserAsync(string userId)
        {
            var response = await Sender.Send(new DeleteUserCommand { UserId = userId });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpGet("all")]
        [ShouldHavePermission(OBNTenantAction.View, OBNTenantFeature.Users)]
        public async Task<IActionResult> GetUsersAsync()
        {
            var response = await Sender.Send(new GetAllUsersQuery());
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpGet("{userId}")]
        [ShouldHavePermission(OBNTenantAction.View, OBNTenantFeature.Users)]
        public async Task<IActionResult> GetUserByIdAsync(string userId)
        {
            var response = await Sender.Send(new GetUserByIdQuery { UserId = userId });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpGet("permissions/{userId}")]
        [ShouldHavePermission(OBNTenantAction.View, OBNTenantFeature.RoleClaims)]
        public async Task<IActionResult> GetUserPermissionsAsync(string userId)
        {
            var response = await Sender.Send(new GetUserPermissionsQuery { UserId = userId });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpGet("user-roles/{userId}")]
        [ShouldHavePermission(OBNTenantAction.View, OBNTenantFeature.UserRoles)]
        public async Task<IActionResult> GetUserRolesAsync(string userId)
        {
            var response = await Sender.Send(new GetUserRolesQuery { UserId = userId });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return NotFound(response);
        }


    }
}
