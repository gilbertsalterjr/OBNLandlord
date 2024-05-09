using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Identity.Users
{
    public interface IUserService
    {
        Task<string> CreateUserAsync(CreateUserRequest request);
        Task<string> UpdateUserAsync(UpdateUserRequest request);
        Task<string> DeleteUserAsync(string userId);
        Task<string> ActivateOrDeactivateAsync(string userId, bool activation);
        Task<string> ChangePasswordAsync(ChangePasswordRequest request);

        Task<string> AssignRolesAsync(string userId, UserRolesRequest request);
        Task<List<UserDto>> GetUsersAsync(CancellationToken ct);
        Task<UserDto> GetUserByIdAsync(string userId, CancellationToken ct);
        Task<List<UserRoleDto>> GetUserRolesAsync(string userId,CancellationToken ct);
        Task<bool> IsEmailTakenAsync(string email);

        Task<List<string>> GetPermissionsAsync(string userId, CancellationToken ct);
        Task<bool> IsPermissionAssignedAsync(string userId, string permission, CancellationToken ct = default);

        
    }
}
