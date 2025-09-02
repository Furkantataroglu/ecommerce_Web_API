using Shared.Entities.Abstract_Base_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;


namespace Shared.Data.Abstract
{
    //bu generic repository
    public interface IEntityRepository<T> where T : class, IEntity ,new()
    {
        Task<T> GetByIdAsync(Guid id);
        //repository.GetAsync(p=>p.Id == 15) gibi bir kullanımı olacak
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties); //predicate null alırsak tüm her şeyi getirir bir parametre verirsek ona göre listeler
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate); //vereceğimiz parametrelere göre tabloda var mı yok mu bakma
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        Task RemoveRangeAsync(IList<T> entities);
        
        // ThenInclude desteği için yeni method'lar
        // ÖRNEK: query => query.Include(c => c.CartItems).ThenInclude(ci => ci.Product)
        Task<T> GetWithIncludesAsync(Expression<Func<T, bool>> predicate, 
            Func<IQueryable<T>, IQueryable<T>> includes = null);
        
        // ThenInclude desteği ile liste getirme
        // ÖRNEK: query => query.Include(p => p.Category).OrderBy(p => p.Name)
        Task<IList<T>> GetAllWithIncludesAsync(Expression<Func<T, bool>> predicate = null, 
            Func<IQueryable<T>, IQueryable<T>> includes = null);
    }
}
