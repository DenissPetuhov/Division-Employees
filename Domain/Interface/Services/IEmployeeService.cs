using Domain.Dto.Users;
using Domain.Result;

namespace Domain.Interface.Services
{
    public interface IEmployeeService
    {
        Task<BaseResult<EmployeeDto>> GetEmployeeAsync();
    }
}
