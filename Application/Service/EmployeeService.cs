using AutoMapper;
using Domain.Dto;
using Domain.Entity;
using Domain.Enums;
using Domain.Interface.Repositories;
using Domain.Interface.Services;
using Domain.Result;
using System.Data;

namespace Application.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IBaseRepositories<Employee> _employeeRepository;
        private readonly IBaseRepositories<Division> _divisionRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IBaseRepositories<Employee> employeeRepository, IBaseRepositories<Division> divisionRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _divisionRepository = divisionRepository;
            _mapper = mapper;
        }
        public BaseResult<EmployeeDto> GetEmployee(int employeeId)
        {
            var response = new BaseResult<EmployeeDto>();
            try
            {
                var data = _employeeRepository.GetAll()
                    .ToList()
                    .FirstOrDefault(x => x.Id == employeeId);
                if (data is null)
                {
                    response.ErrorCode = (int)ErrorCode.DataNotFound;
                    response.ErrorMessage = $"Сотрудник по заданному id={employeeId} не найден.";
                    return response;
                }
                response.Data = _mapper.Map<EmployeeDto>(data);
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResult<EmployeeDto>
                {
                    ErrorCode = (int)ErrorCode.ExceptionService,
                    ErrorMessage = ex.Message,
                };
            }
        }
        public CollectionResult<EmployeeDto> GetEmployeesByDvisionId(int divisionId)
        {
            var response = new CollectionResult<EmployeeDto>();
            try
            {
                var divisiondata = _divisionRepository.GetAll()
                    .FirstOrDefault(x => x.Id == divisionId);
                if (divisiondata == null)
                {
                    response.ErrorCode = (int)ErrorCode.DataNotFound;
                    response.ErrorMessage = $"Отдел по заданному id={divisionId} не найден.";
                    return response;
                }
                var data = _employeeRepository.GetAll()
                    .Where(x => x.DivisionId == divisionId)
                    .Select(_mapper.Map<EmployeeDto>)
                    .ToList();
                if (data == null)
                {
                    response.ErrorCode = (int)ErrorCode.DataNotFound;
                    response.ErrorMessage = $"Сотрудники по заданному id={divisionId} отдела не найден.";
                    return response;
                }
                response.Data = data;
                return response;
            }
            catch (Exception ex)
            {
                return new CollectionResult<EmployeeDto>()
                {
                    ErrorCode = (int)ErrorCode.ExceptionService,
                    ErrorMessage = ex.Message

                };
            }
        }
        public async Task<BaseResult<EmployeeDto>> CreateEmployeeAsync(CreateEmployeeDto employeeDto)
        {
            var response = new BaseResult<EmployeeDto>();
            try
            {
                var division = _divisionRepository.GetAll()
                    .FirstOrDefault(x => x.Id == employeeDto.divisionId);
                if (division is null)
                {
                    response.ErrorCode = (int)ErrorCode.DataNotFound;
                    response.ErrorMessage = $"Отдел по заданному id={employeeDto.divisionId} не найден.";
                    return response;
                }

                var employee = _mapper.Map<Employee>(employeeDto);
                var responsedata = await _employeeRepository.CreateAsync(employee, true);
                response.Data = _mapper.Map<EmployeeDto>(responsedata);
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResult<EmployeeDto>
                {
                    ErrorCode = (int)ErrorCode.ExceptionService,
                    ErrorMessage = ex.Message,
                };
            }
        }
        public async Task<BaseResult<EmployeeDto>> UpdateEmployeeAsync(EmployeeDto employee)
        {
            var response = new BaseResult<EmployeeDto>();
            try
            {
                var data = _employeeRepository.GetAll()
                    .FirstOrDefault(x => x.Id == employee.Id);
                if (data is null)
                {
                    response.ErrorCode = (int)ErrorCode.DataNotFound;
                    response.ErrorMessage = $"Сотрудник по заданному id={employee.Id} не найден.";
                    return response;
                }
                data.FirstName = employee.FirstName;
                data.SecondName = employee.SecondName;
                data.LastName = employee.LastName;
                data.BirthDay = employee.BirthDay;
                data.Position = employee.Position;
                data.DriverLicense = employee.DriverLicense;
                data.Gender = employee.Gender;
                var responsedata = await _employeeRepository.UpdateAsync(data, true);
                response.Data = _mapper.Map<EmployeeDto>(responsedata);
                return response;

            }
            catch (Exception ex)
            {
                return new BaseResult<EmployeeDto>
                {
                    ErrorCode = (int)ErrorCode.ExceptionService,
                    ErrorMessage = ex.Message
                };
            }
        }
        public async Task<BaseResult<EmployeeDto>> DeleteEmployeeAsync(int employeeId)
        {
            var response = new BaseResult<EmployeeDto>();
            try
            {
                var data = _employeeRepository.GetAll()
                    .FirstOrDefault(x => x.Id == employeeId);
                if (data is null)
                {
                    response.ErrorCode = (int)ErrorCode.DataNotFound;
                    response.ErrorMessage = $"Сотрудник по заданному id={employeeId} не найден.";
                    return response;
                }
                var resposedata = await _employeeRepository.RemoveAsync(data, true);
                response.Data = _mapper.Map<EmployeeDto>(resposedata);
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResult<EmployeeDto>
                {
                    ErrorCode = (int)ErrorCode.ExceptionService,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
