using AutoMapper;
using Domain.Dto;
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
        private readonly IMapper mapper;
        public EmployeeController(IEmployeeService employeeService, IDivisionService decisionService)
        {
            _employeeService = employeeService;
            _divisionService = decisionService;
        }
        [HttpGet("get-employee-by-id id={employeeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<Employee>>> GetEmployeeAsync(int employeeId)
        {
            var response = await _employeeService.GetEmployeeAsync(employeeId);
            if (response.isSuccses) return Ok(response);

            return BadRequest(response);
        }
        [HttpGet("get-division-by-id id={divisionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<Employee>>> GetDivionAsync(int divisionId)
        {
            var response = await _divisionService.GetDivisionAsync(divisionId);
            if (response.isSuccses) return Ok(response);

            return BadRequest(response);
        }
        [HttpGet("get-all-division")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CollectionResult<Division>>> GetAllDivision()
        {
            var response = await _divisionService.GetAllDivisionsAsync();
            if (response.isSuccses) return Ok(response);

            return BadRequest(response);
        }
        [HttpGet("get-employee-by-divisionId id={divisionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CollectionResult<Division>>> GetEmployeeByDivisionId(int divisionId)
        {
            var response = await _employeeService.GetEmployeesToDvisionIdAsync(divisionId);
            if (response.isSuccses) return Ok(response);

            return BadRequest(response);
        }

        [HttpPost("add-division")]
        public async Task<ActionResult<BaseResult>> AddDivision(CreateDivisionDto divisionDto)
        {
            var response = await _divisionService.CreateDivisionAsync(divisionDto);
            if (response.isSuccses) return Ok(response);

            return BadRequest(response);
        }
        [HttpPost("add-parent-division")]
        public async Task<ActionResult<BaseResult>> AddParentDivisionAsync(AddParentDivisionDto divisionDto)
        {
            var response = await _divisionService.AddParentDivision(divisionDto);
            if (response.isSuccses) return Ok(response);

            return BadRequest(response);
        }

    }
}
