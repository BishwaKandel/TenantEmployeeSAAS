using Application.Features.EmployeeFeature.Command.CreateEmployee;
using Application.Features.EmployeeFeature.Command.DeleteEmployee;
using Application.Features.EmployeeFeature.Command.UpdateEmployee;
using Application.Features.EmployeeFeature.Query.GetAllEmployees;
using Microsoft.AspNetCore.Mvc;
using Multi_Tenant_WebAPI.Controllers;

namespace Multi_Employee_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class EmployeeController : BaseController
    {

        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand command, CancellationToken token)
        {
            return Ok(await Mediator.Send(command, token));
        }


        [HttpPost("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeCommand command, CancellationToken token)
        {
            return Ok(await Mediator.Send(command, token));
        }

        [HttpPost("DeleteEmployee")]

        public async Task<IActionResult> DeleteEmployee([FromBody] DeleteEmployeeCommand command, CancellationToken token)
        {
            return Ok(await Mediator.Send(command, token));
        }

        [HttpGet("ViewAllEmployees")]

        public async Task<IActionResult> GetAllEmployees()
        {
            return Ok(await Mediator.Send(new GetAllEmployeesQuery()));
        }

        //[HttpGet("GetEmployeeById")]
        //public async Task<IActionResult> GetEmployeeById(string Id)
        //{
        //    return Ok(await Mediator.Send(new GetEmployeeByIdQuery(Id)));
        //}


    }
}
