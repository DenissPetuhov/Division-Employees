using AutoMapper;
using Domain.Dto;
using Domain.Entity;
using Domain.Enums;
using Domain.Interface.Repositories;
using Domain.Interface.Services;
using Domain.Result;
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
                    .Where(x => x.ParentDivisionId == null)
                    .Select(x => _mapper.Map<DivisionDtoTree>(x));

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
                var data = _divisionRepository.GetAllQuaryble()
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
                if (divisionDto.ParentDivisionId.HasValue)
                {
                    var checkShildResponse = ChekingForChild(data, divisionDto.ParentDivisionId.Value);
                    if (!checkShildResponse.isSuccess)
                        return checkShildResponse;
                }
                data.Description = divisionDto.Description;
                data.Name = divisionDto.Name;
                data.ParentDivisionId = divisionDto.ParentDivisionId;
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
        public CollectionResult<DivisionDtoTree> GetAllFlatDivisions(int? checkDivisionId)
        {
            var response = new CollectionResult<DivisionDtoTree>();
            try
            {
                var data = _divisionRepository.GetAllQuaryble()
                    .Select(x => _mapper.Map<DivisionDtoTree>(x))
                    .ToList();

                var parentlist = new List<int>();
                if(checkDivisionId is not null)
                {
                    var checkDivision = data.FirstOrDefault(x => x.Id == checkDivisionId);
                    parentlist = GetChildList(checkDivision.Id);
                }
                response.Data = data.Where(x => !parentlist.Contains(x.Id) && x.Id != checkDivisionId);
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

        //private async Task<BaseResult<DivisionDto>> AddParentDivisionAsync(AddParentDivisionDto addParentDivisionDto)
        //{
        //    try
        //    {
        //        var response = new BaseResult<DivisionDto>();
        //        Division? parentDivision;
        //        Division? division;
        //        division = _divisionRepository.GetAllQuaryble().FirstOrDefault(x => x.Id == addParentDivisionDto.Id);
        //        if (division is null)
        //        {
        //            response.ErrorCode = (int)ErrorCode.DataNotFound;
        //            response.ErrorMessage = $"Зависимый отдел по заданному id={addParentDivisionDto.Id} не найден.";
        //            return response;
        //        }
        //        parentDivision = _divisionRepository.GetAllQuaryble().FirstOrDefault(x => x.Id == addParentDivisionDto.ParentDivisionId);
        //        if (parentDivision is null)
        //        {
        //            response.ErrorCode = (int)ErrorCode.DataNotFound;
        //            response.ErrorMessage = $"Родительски отдел по заданному id={addParentDivisionDto.Id} не найден.";
        //            return response;
        //        }
        //        var checkShildResponse = ChekingForChild(division, addParentDivisionDto.ParentDivisionId, new List<int>());
        //        if (checkShildResponse.isSuccess)
        //            return checkShildResponse;

        //        division.ParentDivisionId = addParentDivisionDto.ParentDivisionId;
        //        response.Data = _mapper.Map<DivisionDto>(await _divisionRepository.UpdateAsync(division, true));
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        return new BaseResult<DivisionDto>
        //        {
        //            ErrorCode = (int)ErrorCode.ExceptionService,
        //            ErrorMessage = ex.Message
        //        };
        //    }
        //}

        /// <summary>
        /// Проверка у изменяемого объекта явяется ли устанавлеваемый его ребёнком
        /// </summary>
        /// <param name="CheckDivision">Проверяемый отдел</param>
        /// <param name="idChildDivision">Id ребенка который проверяем</param>
        /// <param name="idlist">Ссылка на список для записи</param>
        /// <returns>true если у детей найден это ид, false если ид не найден </returns>
        private BaseResult<DivisionDto> ChekingForChild(Division CheckDivision, int idChildDivision, List<int> idlist = null)
        {
            idlist ??= new List<int>();
            if (!CheckDivision.ParentDivisionId.HasValue)
                return new BaseResult<DivisionDto>();
            else
            {
                idlist.Add(CheckDivision.ParentDivisionId.Value);
                var parent = _divisionRepository.GetAllQuaryble()
                    .FirstOrDefault(x => x.Id == CheckDivision.ParentDivisionId);

                ChekingForChild(parent, idChildDivision, idlist);
                if (idlist.Contains(idChildDivision))
                    return new BaseResult<DivisionDto>()
                    {
                        ErrorCode = (int)ErrorCode.CyclicDependency,
                        ErrorMessage = "Установить зависимость не возможно образуется циклическая зависимость",
                    };
            }

            return new BaseResult<DivisionDto>();
        }
        private List<int> GetChildList(int currentid, List<int> idlist = null)
        {
            idlist ??= new List<int>();
            var data = _divisionRepository.GetAll().FirstOrDefault(x => x.Id == currentid);
            if (data.Divisions.IsNullOrEmpty())
                return idlist;
            foreach (var child in data.Divisions)
            {
                idlist.Add(child.Id);
                idlist.AddRange(GetChildList(child.Id));
            }
            return idlist;
        }

    }
}
