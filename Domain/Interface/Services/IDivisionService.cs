﻿using Domain.Dto;
using Domain.Entity;
using Domain.Result;

namespace Domain.Interface.Services
{
    public interface IDivisionService
    {
        /// <summary>
        /// Получить отдел по id
        /// </summary>
        Task<BaseResult<DivisionDto>> GetDivisionAsync(int divisionId);
        /// <summary>
        /// Получить все отделы 
        /// </summary>
        Task<CollectionResult<DivisionDto>> GetAllDivisionsAsync();
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
        /// Добавить родительский отдел
        /// </summary>
        Task<BaseResult<DivisionDto>> AddParentDivision(AddParentDivisionDto addParentDivisionDto);

    }
}
