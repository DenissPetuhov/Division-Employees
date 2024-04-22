namespace Domain.Interface.Repositories
{
    public interface IBaseRepositories<TEntity>
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity> CreateAsync(TEntity entity,bool saveChanges);
        Task<TEntity> UpdateAsync(TEntity entity, bool saveChanges);
        Task<TEntity> RemoveAsync(TEntity entity, bool saveChanges);
        void SaveChangesAsync();
    }
}
