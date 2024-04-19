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

        [HttpGet("get-employee-by-id id={employeeId}")]
        public async Task<ActionResult<BaseResult<Employee>>> GetEmployeeById(int employeeId)
        {
            var response = await _employeeService.GetEmployeeAsync(employeeId);
            if (response.isSuccses) return Ok(response);

            return BadRequest(response);
        }

        [HttpGet("get-employees-by-divisionId id={divisionId}")]
        public async Task<ActionResult<CollectionResult<Employee>>> GetEmployeesByDivisionId(int divisionId)
        {
            var response = await _employeeService.GetEmployeesByDvisionIdAsync(divisionId);
            if (response.isSuccses) return Ok(response);

            return BadRequest(response);
        }

        [HttpPut("update-employee")]
        public async Task<ActionResult<BaseResult>> UpdateEmployee(EmployeeDto employee)
        {
            var response = await _employeeService.UpdateEmployeeAsync(employee);
            if (response.isSuccses) return Ok(response);

            return BadRequest(response);
        }

        [HttpPost("create-employee")]
        public async Task<ActionResult<BaseResult>> CreateEmployee(CreateEmployeeDto employee)
        {
            var response = await _employeeService.CreateEmployeeAsync(employee);
            if (response.isSuccses) return Ok(response);

            return BadRequest(response);
        }

        [HttpDelete("delete-employee id={id}")]
        public async Task<ActionResult<BaseResult>> DeleteEmployee(int id)
        {
            var response = await _employeeService.DeleteEmployeeAsync(id);
            if (response.isSuccses) return Ok(response);

            return BadRequest(response);
        }

    }
}
