using AutoMapper;
using Domain.Dto;
using Domain.Entity;
using Domain.Enums;
using Domain.Interface.Repositories;
using Domain.Interface.Services;
using Domain.Result;
using Microsoft.EntityFrameworkCore;

namespace Application.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IBaseRepositories<Employee> _employeeService;
        private readonly IBaseRepositories<Division> _divisionService;
        private readonly IMapper _mapper;

        public EmployeeService(IBaseRepositories<Employee> employeeService, IBaseRepositories<Division> divisionService, IMapper mapper)
        {
            _employeeService = employeeService;
            _divisionService = divisionService;
            _mapper = mapper;
        }
        public async Task<BaseResult<EmployeeDto>> GetEmployeeAsync(int employeeId)
        {
            var response = new BaseResult<EmployeeDto>();
            try
            {
                response.Data = await _employeeService.GetAll()
                    .Select(x => _mapper.Map<EmployeeDto>(x))
                    .FirstOrDefaultAsync(x => x.EmployeeId == employeeId);
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResult<EmployeeDto>
                {
                    ErrorCode = (int)ErrorCode.ServiceError,
                    ErrorMessage = ex.Message,

                };
            }
        }
        public async Task<CollectionResult<EmployeeDto>> GetEmployeesToDvisionIdAsync(int divisionId)
        {
            var response = new CollectionResult<EmployeeDto>();
            try
            {
                var data = await _employeeService.GetAll()
                    .Where(x => x.Division.Id == divisionId)
                    .Select(x => _mapper.Map<EmployeeDto>(x)).ToArrayAsync();
                if (data != null)
                {
                    response.Data = data;
                    response.ErrorCode = (int)ErrorCode.NoError;
                }
                return response;
            }
            catch (Exception ex)
            {
                return new CollectionResult<EmployeeDto>()
                {
                    ErrorCode = (int)ErrorCode.ServiceError,
                    ErrorMessage = ex.Message

                };
            }
        }
        public async Task<BaseResult<EmployeeDto>> CreateEmployeeAsync(CreateEmployeeDto employeeDto)
        {
            var response = new BaseResult<EmployeeDto>();
            try
            {
                var division = _divisionService.GetAll().FirstOrDefaultAsync(x => x.Id == employeeDto.divisionId);
                if (division.Result == null)
                {
                    response.ErrorCode = (int)ErrorCode.NoDataFound;
                    response.ErrorMessage = $"Отдел по заданному id={employeeDto.divisionId} не найден.";
                    return response;
                }

                var employee = _mapper.Map<Employee>(employeeDto);
                var responsedata = await _employeeService.CreateAsync(employee);

                response.Data = _mapper.Map<EmployeeDto>(responsedata);
                return response;
            }
            catch (Exception ex)
            {

                return new BaseResult<EmployeeDto>
                {
                    ErrorCode = (int)ErrorCode.ServiceError,
                    ErrorMessage = ex.Message,
                };
            }

        }
        public async Task<BaseResult<EmployeeDto>> UpdateEmployeeAsync(EmployeeDto employee)
        {
            var response = new BaseResult<EmployeeDto>();
            try
            {
                var data = await _employeeService.GetAll().FirstOrDefaultAsync(x => x.Id == employee.EmployeeId);
                if (data == null)
                {
                    response.ErrorCode = (int)ErrorCode.NoDataFound;
                    response.ErrorMessage = $"Работник по заданному id={employee.EmployeeId} не найден.";
                    return response;
                }
                var responsedata = await _employeeService.UpdateAsync(data);
                response.Data = _mapper.Map<EmployeeDto>(responsedata);
                return response;

            }
            catch (Exception ex)
            {
                return new BaseResult<EmployeeDto>
                {
                    ErrorCode = (int)ErrorCode.ServiceError,
                    ErrorMessage = ex.Message,
                };
            }
        }
        public async Task<BaseResult<EmployeeDto>> DeleteEmployeeAsync(int employeeId)
        {
            var response = new BaseResult<EmployeeDto>();
            try
            {
                var data = await _employeeService.GetAll().FirstOrDefaultAsync(x => x.Id == employeeId);
                if (data == null)
                {
                    response.ErrorCode = (int)ErrorCode.NoDataFound;
                    response.ErrorMessage = $"Работник по заданному id={employeeId} не найден.";
                    return response;
                }

                var resposedata = await _employeeService.RemoveAsync(data);
                response.Data = _mapper.Map<EmployeeDto>(resposedata);
                return response;


            }
            catch (Exception ex)
            {

                return new BaseResult<EmployeeDto>
                {
                    ErrorCode = (int)ErrorCode.ServiceError,
                    ErrorMessage = ex.Message,
                };
            }

        }


    }
}
