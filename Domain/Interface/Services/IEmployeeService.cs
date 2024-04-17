using Domain.Dto;
using Domain.Entity;
using Domain.Result;

namespace Domain.Interface.Services
{
    public interface IEmployeeService
    {
        /// <summary>
        /// Получение работника по id
        /// </summary>
        Task<BaseResult<EmployeeDto>> GetEmployeeAsync(int employeeId);
        /// <summary>
        /// Получение всех рабоников по id отдела
        /// </summary>
        Task<CollectionResult<EmployeeDto>> GetEmployeesToDvisionIdAsync(int divisioniD);
        /// <summary>
        /// Создание работника 
        /// </summary>
        Task<BaseResult<EmployeeDto>> CreateEmployeeAsync(CreateEmployeeDto employee);
        /// <summary>
        /// Обновить данные работника
        /// </summary>
        Task<BaseResult<EmployeeDto>> UpdateEmployeeAsync(EmployeeDto employee);
        /// <summary>
        /// Удаление работнкика про id
        /// </summary>
        Task<BaseResult<EmployeeDto>> DeleteEmployeeAsync(int employeeId);
    }
}

