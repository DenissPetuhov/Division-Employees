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
                    ErrorMessage = ex.Message,
                };
            }
        }

        public async Task<BaseResult<DivisionDto>> DeleteDivisionAsync(int divisionId)
        {
            var response = new BaseResult<DivisionDto>();
            try
            {
                var data = await _divisionService.GetAll().FirstOrDefaultAsync(x => x.DivisionId == divisionId);
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
                    .FirstOrDefaultAsync(x => x.DivisionId == divisionId);
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
                var data = await _divisionService.GetAll().FirstOrDefaultAsync(x => x.DivisionId == division.DivisionId);
                if (data == null)
                {
                    response.ErrorCode = (int)ErrorCode.NoDataFound;
                    response.ErrorMessage = $"Отдел по заданному id={division.DivisionId} не найден.";
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

    }
}
