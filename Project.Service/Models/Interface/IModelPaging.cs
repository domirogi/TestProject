using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Models.Interface
{
   public interface IModelPaging
    {
        int PageSize { get; set; }
        int Page { get; set; }
    }
}
