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
        [HttpGet("{id}")]
        public ActionResult<BaseResult<Employee>> GetEmployeeById(int id)
        {
            var response =  _employeeService.GetEmployee(id);
            if (response.isSuccess) return Ok(response);

            return BadRequest(response);
        }
        [HttpGet]
        public ActionResult<CollectionResult<Employee>> GetEmployeesByDivisionId(int id)
        {
            var response =  _employeeService.GetEmployeesByDvisionId(id);
            if (response.isSuccess) return Ok(response);

            return BadRequest(response);
        }

        [HttpPut]
        public async Task<ActionResult<BaseResult>> UpdateEmployeeAsync(EmployeeDto employee)
        {
            var response = await _employeeService.UpdateEmployeeAsync(employee);
            if (response.isSuccess) return Ok(response);

            return BadRequest(response);
        }

        [HttpPost]
        public async Task<ActionResult<BaseResult>> CreateEmployeeAsync(CreateEmployeeDto employee)
        {
            var response = await _employeeService.CreateEmployeeAsync(employee);
            if (response.isSuccess) return Ok(response);

            return BadRequest(response);
        }

        [HttpDelete]
        public async Task<ActionResult<BaseResult>> DeleteEmployeeAsync(int id)
        {
            var response = await _employeeService.DeleteEmployeeAsync(id);
            if (response.isSuccess) return Ok(response);

            return BadRequest(response);
        }

    }
}
