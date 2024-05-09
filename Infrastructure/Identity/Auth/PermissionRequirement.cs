using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Auth
{
    internal class PermissionRequirement(string permission) : IAuthorizationRequirement
    {
        public string Permission {  get; set; } = permission;
    }
}
