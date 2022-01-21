using Project.Service.Models;
using Project.Service.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Project.Service.Repository.IRepository
{
    public interface IModelRepository : IRepository<VehicleModel>
    {
       
        Task DeleteModelAsync(int id);
        Task<VehicleModel> GetModelDetailsAsync(int id);
        Task<IPagedList<VehicleModel>> FindModelsAsync(IFilterModel filtering, IModelSorting sorting, IModelPaging paging);
    }
}
