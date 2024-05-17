using Domain.Dto;
using Domain.Result;

namespace Domain.Interface.Services
{
    public interface IDivisionService
    {
        /// <summary>
        /// Получить отдел по id
        /// </summary>
        BaseResult<DivisionDto> GetDivision(int divisionId);
        /// <summary>
        /// Получить дерево всех отделов 
        /// </summary>
        CollectionResult<DivisionDtoTree> GetAllDivisions();
        /// <summary>
        /// Создать отдел
        /// </summary>
        Task<BaseResult<DivisionDto>> CreateDivisionAsync(CreateDivisionDto division);
        /// <summary>
        /// Обновить отдел
        /// </summary>
        Task<BaseResult<DivisionDto>> UpdateDivisionAsync(DivisionDto division);
        /// <summary>
        /// Удалить отдел
        /// </summary>
        Task<BaseResult<DivisionDto>> DeleteDivisionAsync(int divisionId);
        /// <summary>
        /// Получить плоский список отделов
        /// </summary>
        CollectionResult<DivisionDtoTree> GetAllFlatDivisions(int? checkDivisonId);
    }
}
