using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Identity.Users
{
    public interface ICurrentUserService
    {
        string  Name { get; }
        string GetUserId();
        string GetUserEmail();
        string GetUserTenant();
        bool IsAuthenticated();
        bool IsInRole(string roleName);
        IEnumerable<Claim> GetClaims();
        void SetCurrentUser(ClaimsPrincipal principal);
    }
}
