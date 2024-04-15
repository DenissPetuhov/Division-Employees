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
        /// <param name="EmployeeId">Id рабоника</param>
        /// <returns></returns>
        Task<BaseResult<EmployeeDto>> GetEmployeeAsync(int employeeId);
        /// <summary>
        /// Получение всех рабоников по id отдела
        /// </summary>
        /// <param name="divisioniD">id отдела</param>
        /// <returns></returns>
        Task<CollectionResult<EmployeeDto>> GetEmployeesToDvisionIdAsync(int divisioniD);
        /// <summary>
        /// Создание работника 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        Task<BaseResult<EmployeeDto>> CreateEmployeeAsync(CreateEmployeeDto employee);
        /// <summary>
        /// Обновить данные работника
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        Task<BaseResult<EmployeeDto>> UpdateEmployeeAsync(EmployeeDto employee);
        /// <summary>
        /// Удаление работнкика про id
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        Task<BaseResult<EmployeeDto>> DeleteEmployeeAsync(int employeeId);
      


    }
}

