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
    public interface IMakeRepository : IRepository<VehicleMake>
    {
       
        Task DeleteMakeAsync(int id);
        Task<IPagedList<VehicleMake>> FindMakeAsync(IFilterModel filtering, IModelSorting sorting, IModelPaging paging);
    }
}
