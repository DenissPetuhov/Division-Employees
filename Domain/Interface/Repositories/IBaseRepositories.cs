namespace Domain.Interface.Repositories
{
    public interface IBaseRepositories<TEntity>
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity> CreateAsync(TEntity entity);
        TEntity Update(TEntity entity);
        void Remove(TEntity entity);    
        Task<int> SaveChangesAsync();
    }
}
