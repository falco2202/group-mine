using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected readonly GroupMineDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(GroupMineDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}