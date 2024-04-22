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

        [HttpGet("get-by-id")]
        public async Task<ActionResult<BaseResult<Employee>>> GetEmployeeByIdAsync(int employeeId)
        {
            var response = await _employeeService.GetEmployeeAsync(employeeId);
            if (response.isSuccses) return Ok(response);

            return BadRequest(response);
        }

        [HttpGet("get-by-divisionId")]
        public async Task<ActionResult<CollectionResult<Employee>>> GetEmployeesByDivisionIdAsync(int divisionId)
        {
            var response = await _employeeService.GetEmployeesByDvisionIdAsync(divisionId);
            if (response.isSuccses) return Ok(response);

            return BadRequest(response);
        }

        [HttpPut("update")]
        public async Task<ActionResult<BaseResult>> UpdateEmployeeAsync(EmployeeDto employee)
        {
            var response = await _employeeService.UpdateEmployeeAsync(employee);
            if (response.isSuccses) return Ok(response);

            return BadRequest(response);
        }

        [HttpPost("create")]
        public async Task<ActionResult<BaseResult>> CreateEmployeeAsync(CreateEmployeeDto employee)
        {
            var response = await _employeeService.CreateEmployeeAsync(employee);
            if (response.isSuccses) return Ok(response);

            return BadRequest(response);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<BaseResult>> DeleteEmployeeAsync(int id)
        {
            var response = await _employeeService.DeleteEmployeeAsync(id);
            if (response.isSuccses) return Ok(response);

            return BadRequest(response);
        }

    }
}
