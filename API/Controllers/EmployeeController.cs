using Domain.Entity;
using Domain.Interface.Services;
using Domain.Result;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDivisionService _divisionService;
        public EmployeeController(IEmployeeService employeeService, IDivisionService decisionService)
        {
            _employeeService = employeeService;
            _divisionService = decisionService;
        }
        [HttpGet("GetEmployeeById id={employeeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<Employee>>> GetEmployeeAsync(int employeeId)
        {
            var response = await _employeeService.GetEmployeeAsync(employeeId);
            if (response.isSuccses) return Ok(response);

            return BadRequest(response);
        }
        [HttpGet("GetDivisionById id={divisionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<Employee>>> GetDivionAsync(int divisionId)
        {
            var response = await _divisionService.GetDivisionAsync(divisionId);
            if (response.isSuccses) return Ok(response);

            return BadRequest(response);
        }


    }
}
