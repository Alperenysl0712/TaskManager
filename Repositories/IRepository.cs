using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TaskManager.Utility;

namespace TaskManager.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByConditionAsync(Expression<Func<T, bool>> predicate);
        Task Ekle(T entity);
        Task Guncelle(T entity);
        Task Sil(int id);
    }
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DataBaseContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(DataBaseContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }


        public async Task Ekle(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByConditionAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }


        public async Task Guncelle(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Sil(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
