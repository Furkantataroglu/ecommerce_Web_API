using Microsoft.EntityFrameworkCore;
using Shared.Data.Abstract;
using Shared.Entities.Abstract_Base_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Data.Concrete.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity> : IEntityRepository<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly DbContext _context;
        public EfEntityRepositoryBase(DbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().AnyAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().CountAsync(predicate);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await Task.Run(() => { _context.Set<TEntity>().Remove(entity); });
        }

        public async Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            if(predicate != null)
            {
                query = query.Where(predicate);
            }
            if(includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            return await query.ToListAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            return await query.SingleOrDefaultAsync();
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task RemoveRangeAsync(IList<TEntity> entities)
        {
            await Task.Run(() => { _context.Set<TEntity>().RemoveRange(entities); });
        }

        // ThenInclude desteği ile tek entity getirme
        // ÖRNEK: query => query.Include(c => c.CartItems).ThenInclude(ci => ci.Product)
        public async Task<TEntity> GetWithIncludesAsync(Expression<Func<TEntity, bool>> predicate, 
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (includes != null)
            {
                query = includes(query);
            }
            return await query.SingleOrDefaultAsync();
        }

        // ThenInclude desteği ile liste getirme
        // ÖRNEK: query => query.Include(p => p.Category).OrderBy(p => p.Name)
        public async Task<IList<TEntity>> GetAllWithIncludesAsync(Expression<Func<TEntity, bool>> predicate = null, 
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (includes != null)
            {
                query = includes(query);
            }
            return await query.ToListAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() => { _context.Set<TEntity>().Update(entity); });
        }

  
    }
}
