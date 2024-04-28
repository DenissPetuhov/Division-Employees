namespace Domain.Interface.Repositories
{
    public interface IBaseRepositories<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        IQueryable<TEntity> GetAllQuaryble();
        Task<TEntity> CreateAsync(TEntity entity,bool saveChanges);
        Task<TEntity> UpdateAsync(TEntity entity, bool saveChanges);
        Task<TEntity> RemoveAsync(TEntity entity, bool saveChanges);
    }
}
