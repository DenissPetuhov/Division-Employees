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
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;

        }
        //[HttpPost("create")]
        //public async Task<ActionResult<BaseResult<Division>>> GetEmployeeByDivisionId(int divisionId)
        //{

        //}


        [HttpGet("get-employee-by-id id={employeeId}")]
        public async Task<ActionResult<BaseResult<Employee>>> GetEmployeeAsync(int employeeId)
        {
            var response = await _employeeService.GetEmployeeAsync(employeeId);
            if (response.isSuccses) return Ok(response);

            return BadRequest(response);
        }

      
        [HttpGet("get-employees-by-divisionId id={divisionId}")]
        public async Task<ActionResult<CollectionResult<Division>>> GetEmployeesByDivisionId(int divisionId)
        {
            var response = await _employeeService.GetEmployeesByDvisionIdAsync(divisionId);
            if (response.isSuccses) return Ok(response);

            return BadRequest(response);
        }

        //[HttpPost("create")]
    




    }
}
