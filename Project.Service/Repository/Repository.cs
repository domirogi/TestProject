using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Project.Service.DataAccess.Data;
using Project.Service.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Project.Service.Repository
{
    public  class Repository<T> : IRepository<T> where T : class, IEntityBase, new()
    {
        private readonly VehicleDbContext _db;
        private readonly DbSet<T> _dbSet;
        public Repository(VehicleDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _db.Set<T>().AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _db.Set<T>().FirstOrDefaultAsync(m => m.Id == id);
            EntityEntry entityEntry = _db.Entry<T>(entity);
            entityEntry.State = EntityState.Deleted;

            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _db.Set<T>().ToListAsync();

        public async Task<T> GetByIdAsync(int id) => await _db.Set<T>().FirstOrDefaultAsync(m => m.Id == id);


        public async Task<IPagedList<T>>  GetPageFilterAsync(int pageNumber, int pageSize, Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            var count = await GetByCondition(filter).CountAsync();
            var entities = await GetByCondition(filter, orderBy, includeProperties,
                pageSize * (pageNumber - 1), pageSize).AsNoTracking().ToListAsync();
            return new StaticPagedList<T>(entities, pageNumber, pageSize, count);
        }

        public  Task<T> GetWithDetailsAsync(Expression<Func<T, bool>> filter, string includeProperties = null)
        {
            return GetByCondition(filter, null, includeProperties).SingleOrDefaultAsync();
        }

        public async Task UpdateAsync(int id, T entity)
        {
            EntityEntry entityEntry = _db.Entry<T>(entity);
            entityEntry.State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }
        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           string includeProperties = null,
           int? skip = null,
           int? take = null)
        {
            includeProperties ??= string.Empty;
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;
        }
    }
}
