using Project.Service.DataAccess.Data;
using Project.Service.Models;
using Project.Service.Models.Interface;
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
    public class MakeRepository : Repository<VehicleMake>, IMakeRepository
    {
        public MakeRepository(VehicleDbContext db) : base(db) { }


        public async Task DeleteMakeAsync(int id) => await DeleteAsync(id);


        public Task<IPagedList<VehicleMake>> FindMakeAsync(IFilterModel filtering, IModelSorting sorting, IModelPaging paging)
        {
            Expression<Func<VehicleMake, bool>> filter = null;
            if (!string.IsNullOrWhiteSpace(filtering.Filter))
            {
                filter = m => m.Name.ToUpper().Contains(filtering.Filter.ToUpper()) ||
                                m.Abrv.ToUpper().Contains(filtering.Filter.ToUpper());
            }
            Func<IQueryable<VehicleMake>, IOrderedQueryable<VehicleMake>> orderBy = sorting.Sort switch
            {
                "Name" => q => q.OrderBy(m => m.Name),
                "name_desc" => q => q.OrderByDescending(m => m.Name),
                "Abrv" => q => q.OrderBy(m => m.Abrv),
                "abrv_desc" => q => q.OrderByDescending(m => m.Abrv),
                _ => null,
            };

            return GetPageFilterAsync(paging.Page, paging.PageSize, filter, orderBy);
        }


    }
}
