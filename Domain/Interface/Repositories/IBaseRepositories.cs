namespace Domain.Interface.Repositories
{
    public interface IBaseRepositories<TEntity>
    {
        /// <summary>
        /// Получить весь список объектов
        /// </summary>
        IEnumerable<TEntity> GetAll();
        /// <summary>
        /// Создать объект
        /// </summary>
        /// <param name="saveChanges">Сохранить изменения</param>
        Task<TEntity> CreateAsync(TEntity entity, bool saveChanges);
        /// <summary>
        /// Обновить объект
        /// </summary>
        /// <param name="saveChanges">Сохранить изменения</param>
        Task<TEntity> UpdateAsync(TEntity entity, bool saveChanges);
        /// <summary>
        /// Удалить ебъект
        /// </summary>
        /// <param name="saveChanges">Сохранить изменения</param>
        Task<TEntity> RemoveAsync(TEntity entity, bool saveChanges);
    }
}
