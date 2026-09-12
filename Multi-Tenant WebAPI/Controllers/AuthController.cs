using Application.Features.AuthFeature.Command.LoginUser;
using Application.Features.AuthFeature.Command.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Multi_Tenant_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("Register")]

        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
