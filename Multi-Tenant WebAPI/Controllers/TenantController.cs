using Application.Features.TenantFeature.Command.CreateTenant;
using Application.Features.TenantFeature.Command.DeleteTenant;
using Application.Features.TenantFeature.Command.UpdateTenant;
using Application.Features.TenantFeature.Query.GetAllTenants;
using Application.Features.TenantFeature.Query.GetTenantById;
using Domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Multi_Tenant_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TenantController : BaseController
    {
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("CreateTenant")]
        public async Task<IActionResult> CreateTenant([FromBody] CreateTenantCommand command , CancellationToken token )
        {
            return Ok(await Mediator.Send(command, token));
        }


        [HttpPost("UpdateTenant")]
        public async Task<IActionResult> UpdateTenant([FromBody] UpdateTenantCommand command, CancellationToken token)
        {
            return Ok(await Mediator.Send(command, token));
        }

        [HttpPost("DeleteTenant")]

        public async Task<IActionResult> DeleteTenant([FromBody] DeleteTenantCommand command, CancellationToken token)
        {
            return Ok(await Mediator.Send(command, token));
        }

        [HttpGet("ViewAllTenants")]

        public async Task<IActionResult> GetAllTenants()
        {
            return Ok(await Mediator.Send(new GetAllTenantsQuery()));
        }

        [HttpGet("GetTenantById")]
        public async Task<IActionResult> GetTenantById(string Id)
        {
            return Ok(await Mediator.Send(new GetTenantByIdQuery(Id)));
        }
    }
}
