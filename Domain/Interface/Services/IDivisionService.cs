using Domain.Dto;
using Domain.Entity;
using Domain.Result;

namespace Domain.Interface.Services
{
    public interface IDivisionService
    {
        /// <summary>
        /// Получить отдел по id
        /// </summary>
        /// <param name="divisionId"></param>
        /// <returns></returns>
        Task<BaseResult<DivisionDto>> GetDivisionAsync(int divisionId);
        /// <summary>
        /// Получить все отделы 
        /// </summary>
        /// <returns></returns>
        Task<CollectionResult<DivisionDto>> GetAllDivisionsAsync();
        /// <summary>
        /// Создать отдел
        /// </summary>
        /// <param name="divisionDto"></param>
        /// <returns></returns>
        Task<BaseResult<DivisionDto>> CreateDivisionAsync(CreateDivisionDto division);
        /// <summary>
        /// Обновить отдел
        /// </summary>
        /// <param name="division"></param>
        /// <returns></returns>
        Task<BaseResult<DivisionDto>> UpdateDivisionAsync(DivisionDto division);
        /// <summary>
        /// Удалить отдел
        /// </summary>
        /// <param name="divideId"></param>
        /// <returns></returns>
        Task<BaseResult<DivisionDto>> DeleteDivisionAsync(int divisionId);

    }
}
