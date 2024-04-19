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
        private readonly IBaseRepositories<Division> _divisionService;
        private readonly IMapper _mapper;

        public DivisionService(IBaseRepositories<Division> divisionService, IMapper mapper)
        {
            _divisionService = divisionService;
            _mapper = mapper;
        }
        public async Task<BaseResult<DivisionDto>> CreateDivisionAsync(CreateDivisionDto divisiondto)
        {
            var response = new BaseResult<DivisionDto>();
            try
            {
                var division = _mapper.Map<Division>(divisiondto);
                division.DateCreate = DateTime.Now;
                var responseDivision = await _divisionService.CreateAsync(division);
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
                var data = await _divisionService.GetAll().FirstOrDefaultAsync(x => x.Id == divisionId);
                if (data is null)
                {
                    response.ErrorCode = (int)ErrorCode.DataNotFound;
                    response.ErrorMessage = $"Отдел по заданному id={divisionId} не найден.";
                    return response;
                }
                var responsedata = await _divisionService.RemoveAsync(data);
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
        public async Task<CollectionResult<DivisionDto>> GetAllDivisionsAsync()
        {
            var response = new CollectionResult<DivisionDto>();
            try
            {
                var data = await _divisionService.GetAll()
                    .Select(x => _mapper.Map<DivisionDto>(x))
                    .ToArrayAsync();
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
                return new CollectionResult<DivisionDto>()
                {
                    ErrorCode = (int)ErrorCode.ExceptionService,
                    ErrorMessage = ex.Message
                };
            }
        }
        public async Task<BaseResult<DivisionDto>> GetDivisionAsync(int divisionId)
        {
            var response = new BaseResult<DivisionDto>();
            try
            {
                var data = await _divisionService.GetAll()
                    .Select(x => _mapper.Map<DivisionDto>(x))
                    .FirstOrDefaultAsync(x => x.Id == divisionId);
                if (data is null)
                {
                    response.ErrorCode = (int)ErrorCode.DataNotFound;
                    response.ErrorMessage = $"Отдел по заданному id={divisionId} не найден.";
                    return response;
                }
                response.Data = data;
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
        public async Task<BaseResult<DivisionDto>> UpdateDivisionAsync(DivisionDto division)
        {
            var response = new BaseResult<DivisionDto>();
            try
            {
                var data = await _divisionService.GetAll().FirstOrDefaultAsync(x => x.Id == division.Id);
                if (data == null)
                {
                    response.ErrorCode = (int)ErrorCode.DataNotFound;
                    response.ErrorMessage = $"Отдел по заданному id={division.Id} не найден";
                    return response;
                }
                var responsedata = await _divisionService.UpdateAsync(data);
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
        public async Task<BaseResult<DivisionDto>> AddParentDivision(AddParentDivisionDto addParentDivisionDto)
        {
            try
            {
                var response = new BaseResult<DivisionDto>();
                //Родительский отдел
                Division? parentDivision;
                //Зависимый отдел
                Division? division;
                //Вызов сущности зависимого отдела
                division = _divisionService.GetAll().FirstOrDefault(x => x.Id == addParentDivisionDto.Id);
                if (division is null)
                {
                    response.ErrorCode = (int)ErrorCode.DataNotFound;
                    response.ErrorMessage = $"Зависимый отдел по заданному id={addParentDivisionDto.Id} не найден.";
                    return response;
                }
                //Вызов сущности родительского отдела
                parentDivision = _divisionService.GetAll().FirstOrDefault(x => x.Id == addParentDivisionDto.ParentDivisionId);
                if (parentDivision is null)
                {
                    response.ErrorCode = (int)ErrorCode.DataNotFound;
                    response.ErrorMessage = $"Родительски отдел по заданному id={addParentDivisionDto.Id} не найден.";
                    return response;
                }
                // Установка зависимости 
                if (ChekingForChild(division, addParentDivisionDto.ParentDivisionId, new List<int>()))
                {
                    response.ErrorCode = (int)ErrorCode.CyclicDependency;
                    response.ErrorMessage = "Установть зависимость не возможно образуется циклическая зависимость";
                    return response;
                }
                response.Data =  _mapper.Map<DivisionDto>(await _divisionService.UpdateAsync(division));
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
