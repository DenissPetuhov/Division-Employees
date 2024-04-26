using Domain.Interface.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepositories<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async void SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException("Entity is null");
            await _context.AddAsync(entity);
            return entity;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();

        }

        public async Task<TEntity> CreateAsync(TEntity entity, bool saveChanges)
        {
            if (entity is null)
                throw new ArgumentNullException("Entity is null");
            await _context.AddAsync(entity);
            if (saveChanges) await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, bool saveChanges)
        {
            if (entity is null)
                throw new ArgumentNullException("Entity is null");
            _context.Update(entity);
            if (saveChanges) await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> RemoveAsync(TEntity entity, bool saveChanges)
        {
            if (entity is null)
                throw new ArgumentNullException("Entity is null");
            _context.Remove(entity);
            if (saveChanges) await _context.SaveChangesAsync();
            return entity;
        }


    }
}
