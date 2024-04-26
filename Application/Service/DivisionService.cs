using AutoMapper;
using Domain.Dto;
using Domain.Entity;
using Domain.Enums;
using Domain.Interface.Repositories;
using Domain.Interface.Services;
using Domain.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Application.Service
{
    public class DivisionService : IDivisionService
    {
        private readonly IBaseRepositories<Division> _divisionRepository;
        private readonly IBaseRepositories<Employee> _employeeRepository;
        private readonly IMapper _mapper;

        public DivisionService(IBaseRepositories<Division> divisionService, IBaseRepositories<Employee> employeeRepository, IMapper mapper)
        {
            _divisionRepository = divisionService;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        public async Task<BaseResult<DivisionDto>> CreateDivisionAsync(CreateDivisionDto divisiondto)
        {
            var response = new BaseResult<DivisionDto>();
            try
            {
                var division = _mapper.Map<Division>(divisiondto);
                division.DateCreate = DateTime.Now;
                var responseDivision = await _divisionRepository.CreateAsync(division, true);
                response.Data = _mapper.Map<DivisionDto>(responseDivision);
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResult<DivisionDto>
                {
                    ErrorCode = (int)ErrorCode.ExceptionService,
                    ErrorMessage = ex.Message
                };
            }
        }
        public async Task<BaseResult<DivisionDto>> DeleteDivisionAsync(int divisionId)
        {
            var response = new BaseResult<DivisionDto>();
            try
            {
                var data = _divisionRepository.GetAllQuaryble()
                    .FirstOrDefault(x => x.Id == divisionId);
                if (data is null)
                {
                    response.ErrorCode = (int)ErrorCode.DataNotFound;
                    response.ErrorMessage = $"Отдел по заданному id={divisionId} не найден.";
                    return response;
                }
                foreach (var obj in data.Divisions)
                {
                    obj.ParentDivision = null;
                    await _divisionRepository.UpdateAsync(obj, false);
                }
                foreach (var obj in data.Employees)
                {
                    await _employeeRepository.RemoveAsync(obj, false);
                }
                var responsedata = await _divisionRepository.RemoveAsync(data, true);
                response.Data = _mapper.Map<DivisionDto>(responsedata);
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResult<DivisionDto>
                {
                    ErrorCode = (int)ErrorCode.ExceptionService,
                    ErrorMessage = ex.Message,
                };
            }
        }
        public CollectionResult<DivisionDtoTree> GetAllDivisions()
        {
            var response = new CollectionResult<DivisionDtoTree>();
            try
            {
                var data = _divisionRepository.GetAll()
                    .Select(x => _mapper.Map<DivisionDtoTree>(x))
                    .ToList()
                    .Where(x => x.Divisions == null);
                if (data is null)
                {
                    response.ErrorCode = (int)ErrorCode.DataNotFound;
                    response.ErrorMessage = "Отделы не найдены";
                    return response;
                }
                response.Data = data;
                return response;
            }
            catch (Exception ex)
            {
                return new CollectionResult<DivisionDtoTree>()
                {
                    ErrorCode = (int)ErrorCode.ExceptionService,
                    ErrorMessage = ex.Message
                };
            }
        }
        public BaseResult<DivisionDto> GetDivision(int divisionId)
        {
            var response = new BaseResult<DivisionDto>();
            try
            {
                var data =  _divisionRepository.GetAllQuaryble()
                    .FirstOrDefault(x => x.Id == divisionId);
                if (data is null)
                {
                    response.ErrorCode = (int)ErrorCode.DataNotFound;
                    response.ErrorMessage = $"Отдел по заданному id={divisionId} не найден.";
                    return response;
                }
                response.Data = _mapper.Map<DivisionDto>(data);
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResult<DivisionDto>
                {
                    ErrorCode = (int)ErrorCode.ExceptionService,
                    ErrorMessage = ex.Message,

                };
            }
        }
        public async Task<BaseResult<DivisionDto>> UpdateDivisionAsync(DivisionDto divisionDto)
        {
            var response = new BaseResult<DivisionDto>();
            try
            {
                var data = _divisionRepository.GetAllQuaryble()
                    .FirstOrDefault(x => x.Id == divisionDto.Id);
                if (data == null)
                {
                    response.ErrorCode = (int)ErrorCode.DataNotFound;
                    response.ErrorMessage = $"Отдел по заданному id={divisionDto.Id} не найден";
                    return response;
                }
                data.Description = divisionDto.Description;
                data.Name = divisionDto.Name;
                var responsedata = await _divisionRepository.UpdateAsync(data, true);
                response.Data = _mapper.Map<DivisionDto>(responsedata);
                return response;

            }
            catch (Exception ex)
            {
                return new BaseResult<DivisionDto>
                {
                    ErrorCode = (int)ErrorCode.ExceptionService,
                    ErrorMessage = ex.Message,
                };
            }
        }
        public async Task<BaseResult<DivisionDto>> AddParentDivisionAsync(AddParentDivisionDto addParentDivisionDto)
        {
            try
            {
                var response = new BaseResult<DivisionDto>();
                Division? parentDivision;
                Division? division;
                division = _divisionRepository.GetAllQuaryble().FirstOrDefault(x => x.Id == addParentDivisionDto.Id);
                if (division is null)
                {
                    response.ErrorCode = (int)ErrorCode.DataNotFound;
                    response.ErrorMessage = $"Зависимый отдел по заданному id={addParentDivisionDto.Id} не найден.";
                    return response;
                }
                parentDivision = _divisionRepository.GetAllQuaryble().FirstOrDefault(x => x.Id == addParentDivisionDto.ParentDivisionId);
                if (parentDivision is null)
                {
                    response.ErrorCode = (int)ErrorCode.DataNotFound;
                    response.ErrorMessage = $"Родительски отдел по заданному id={addParentDivisionDto.Id} не найден.";
                    return response;
                }
                if (ChekingForChild(division, addParentDivisionDto.ParentDivisionId, new List<int>()))
                {
                    response.ErrorCode = (int)ErrorCode.CyclicDependency;
                    response.ErrorMessage = "Установть зависимость не возможно образуется циклическая зависимость";
                    return response;
                }
                division.ParentDivisionId = addParentDivisionDto.ParentDivisionId;
                response.Data = _mapper.Map<DivisionDto>(await _divisionRepository.UpdateAsync(division, true));
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResult<DivisionDto>
                {
                    ErrorCode = (int)ErrorCode.ExceptionService,
                    ErrorMessage = ex.Message
                };
            }
        }

        /// <summary>
        /// Проверка у изменяемого объекта явяется ли устанавлеваемый его ребёнком
        /// </summary>
        /// <param name="CheckDivision">Проверяемый отдел</param>
        /// <param name="idChildDivision">Id ребенка который проверяем</param>
        /// <param name="idlist">Ссылка на список для записи</param>
        /// <returns>true если у детей найден это ид, false если ид не найден </returns>
        private bool ChekingForChild(Division CheckDivision, int idChildDivision, List<int> idlist)
        {
            if (!CheckDivision.Divisions.IsNullOrEmpty())
            {
                foreach (var child in CheckDivision.Divisions)
                {
                    idlist.Add(child.Id);
                    ChekingForChild(child, idChildDivision, idlist);
                }
                if (idlist.Contains(idChildDivision))
                    return true;
            }
            return false;


        }

    }
}
