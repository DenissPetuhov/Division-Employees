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

        public EmployeeController(IEmployeeService userService)
        {
            this._employeeService = userService;
        }
        public async Task<ActionResult<BaseResult<Employee>>> GetEmployeesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
