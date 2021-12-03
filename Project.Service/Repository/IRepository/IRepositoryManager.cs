using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Repository.IRepository
{
   public interface IRepositoryManager
    {
        IMakeRepository Make { get; }
        IModelRepository Model { get; }

        Task SaveAsync();
    }
}
