using Domain.Dto.Users;
using Domain.Entity;
using Domain.Interface.Repositories;
using Domain.Interface.Services;
using Domain.Result;

namespace Application.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IBaseRepositories<Employee> _employeeService;

        public EmployeeService(IBaseRepositories<Employee> employeeService)
        {
            _employeeService = employeeService;
        }

        public Task<BaseResult<EmployeeDto>> GetEmployeeAsync()
        {
            throw new NotImplementedException();
            
        }
    }
}
