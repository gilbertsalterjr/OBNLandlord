using Application.Features.Orgs;
using Application.Features.Orgs.Commands;
using Application.Features.TenantOrgs;
using Application.Features.TenantOrgs.Commands;
using Application.Features.TenantOrgs.Queries;
using Infrastructure.Identity.Auth;
using Infrastructure.Identity.Constants;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class OrgsController : BaseApiController
    {
        [HttpPost("add")]
        [ShouldHavePermission(OBNTenantAction.Create, OBNTenantFeature.OBNTenants)]
        public async Task<IActionResult> CreateOrgAsync([FromBody] CreateOrgRequest createOrg)
        {
            var response = await Sender.Send(new CreateOrgCommand { OrgRequest = createOrg });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPut("update")]
        [ShouldHavePermission(OBNTenantAction.Update, OBNTenantFeature.OBNTenants)]
        public async Task<IActionResult> UpdateOrgAsync([FromBody] UpdateOrgRequest updateOrg)
        {
            var response = await Sender.Send(new UpdateOrgCommand { OrgRequest = updateOrg });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpDelete("delete/{orgId}")]
        [ShouldHavePermission(OBNTenantAction.Delete, OBNTenantFeature.OBNTenants)]
        public async Task<IActionResult> DeleteOrgAsync(int orgId)
        {
            var response = await Sender.Send(new DeleteOrgCommand { OrgId = orgId });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("by-id/{orgId}")]
        [ShouldHavePermission(OBNTenantAction.View, OBNTenantFeature.OBNTenants)]
        public async Task<IActionResult> GetOrgByIdAsync(int orgId)
        {
            var response = await Sender.Send(new GetOrgByIdQuery { OrgId = orgId });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpGet("by-name/{name}")]
        [ShouldHavePermission(OBNTenantAction.View, OBNTenantFeature.OBNTenants)]
        public async Task<IActionResult> GetOrgByNameAsync(string name)
        {
            var response = await Sender.Send(new GetOrgByNameQuery { Name = name });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpGet("all")]
        [ShouldHavePermission(OBNTenantAction.View, OBNTenantFeature.OBNTenants)]
        public async Task<IActionResult> GetAllOrgsAsync()
        {
            var response = await Sender.Send(new GetOrgsQuery());
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return NotFound(response);
        }
    }
}
