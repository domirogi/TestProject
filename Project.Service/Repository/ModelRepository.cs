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
   public class ModelRepository : Repository<VehicleModel>, IModelRepository
    {
        public ModelRepository(VehicleDbContext db) : base(db) { }

        public async Task AddModelAsync(VehicleModel modelAdd) => await AddAsync(modelAdd);


        public async Task DeleteModelAsync(int id) => await DeleteAsync(id);



        public async Task<VehicleModel> GetByIdModelAsync(int id) => await GetByIdAsync(id);
       

        public Task<IPagedList<VehicleModel>> GetFilterModelsAsync(IFilterModel filtering, IModelSorting sorting, IModelPaging paging)
        {
            Expression<Func<VehicleModel, bool>> filter = null;
            if (filtering.FilterId != null)
            {
                filter = m => m.MakeId == filtering.FilterId;
            }
            Func<IQueryable<VehicleModel>, IOrderedQueryable<VehicleModel>> orderBy = sorting.Sort switch
            {
                "Name" => q => q.OrderBy(m => m.Name),
                "name_desc" => q => q.OrderByDescending(m => m.Name),
                "Abrv" => q => q.OrderBy(m => m.Abrv),
                "abrv_desc" => q => q.OrderByDescending(m => m.Abrv),
                "Make" => q => q.OrderBy(m => m.Make.Name),
                "make_desc" => q => q.OrderByDescending(m => m.Make.Name),
                _ => null,
            };

            return GetPageFilterAsync(paging.Page, paging.PageSize, filter, orderBy, "Make");
        }

        public async Task<IEnumerable<VehicleModel>> GetModelAsync() => await  GetAllAsync();


        public Task<VehicleModel> GetModelDetailsAsync(int id) => GetWithDetailsAsync(m => m.Id == id, "Make");


        public Task UpdateModelAsync(int id, VehicleModel modelUpdate) => UpdateAsync(id, modelUpdate);
       
    }
}
