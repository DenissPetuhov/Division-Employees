namespace Domain.Interface.Repositories
{
    public interface IBaseRepositories<TEntity>
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> RemoveAsync(TEntity entity);    
    }
}
