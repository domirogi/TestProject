using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Project.Service.Repository.IRepository
{
    public interface IRepository<T> where T : class, IEntityBase, new()
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(int id, T entity);
        Task DeleteAsync(int id);
        Task<T> GetWithDetailsAsync(Expression<Func<T, bool>> filter, string includeProperties = null);
        Task<IPagedList<T>> GetPageFilterAsync(int pageNumber, int pageSize, Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null);
    }
}
