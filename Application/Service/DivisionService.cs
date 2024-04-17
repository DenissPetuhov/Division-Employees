using AutoMapper;
using Azure;
using Domain.Dto;
using Domain.Entity;
using Domain.Enums;
using Domain.Interface.Repositories;
using Domain.Interface.Services;
using Domain.Result;
using Microsoft.EntityFrameworkCore;

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
                    ErrorCode = (int)ErrorCode.ServiceError,
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
                if (data == null)
                {
                    response.ErrorCode = (int)ErrorCode.NoDataFound;
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
                    ErrorCode = (int)ErrorCode.ServiceError,
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
                    .Select(x => _mapper.Map<DivisionDto>(x)).ToArrayAsync();
                if (data == null)
                {
                    response.ErrorCode = (int)ErrorCode.NoDataFound;
                    response.ErrorMessage = "Данные не найдены";
                    return response;
                }
                response.Data = data;
                return response;
            }
            catch (Exception ex)
            {
                return new CollectionResult<DivisionDto>()
                {
                    ErrorCode = (int)ErrorCode.ServiceError,
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
                if (data == null)
                {
                    response.ErrorCode = (int)ErrorCode.NoDataFound;
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
                    ErrorCode = (int)ErrorCode.ServiceError,
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
                    response.ErrorCode = (int)ErrorCode.NoDataFound;
                    response.ErrorMessage = $"Отдел по заданному id={division.Id} не найден.";
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
                    ErrorCode = (int)ErrorCode.ServiceError,
                    ErrorMessage = ex.Message,
                };
            }
        }
        private BaseResult<Division> CheckRecursionDivision(Division division, List<int> idList)
        {
            var response = new BaseResult<Division>();
            if (division.ParentDivision is null)
            {
                response.Data = division;
                return response;
            }

            foreach (var item in idList)
            {
                if (item == division.Id)
                {
                    response.ErrorMessage = "Зависимость рекурсивна";
                    return response;
                }

            }
            idList.Add(division.Id);

            return CheckRecursionDivision(division.ParentDivision, idList);
        }
        private Division GetEntityDivision(int divisionId)
        {
            var response = _divisionService.GetAll().FirstOrDefault(x => x.Id == divisionId);
            if (response is null)
            {
                throw new NullReferenceException();
            }
            return response;
        }
        public async Task<BaseResult> AddParentDivision(AddParentDivisionDto addParentDivisionDto)
        {
            try
            {
                var division = GetEntityDivision(addParentDivisionDto.Id);
                var parentDivision = GetEntityDivision(addParentDivisionDto.ParentDivisionId);
                division.ParentDivision = parentDivision;
                var result = CheckRecursionDivision(division, new List<int>());
                if (!result.isSuccses) return result;
                await _divisionService.UpdateAsync(division);
                return new BaseResult();

            }
            catch (NullReferenceException)
            {
                return new BaseResult
                {
                    ErrorCode = (int)ErrorCode.ServiceError,
                    ErrorMessage = "Такой записи не существует"
                };
            }
            catch (Exception ex)
            {
                return new BaseResult()
                {
                    ErrorCode = (int)ErrorCode.ServiceError,
                    ErrorMessage = ex.Message
                };
            }

        }
    }
}
