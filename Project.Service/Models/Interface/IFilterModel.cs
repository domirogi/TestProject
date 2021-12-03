using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Models.Interface
{
    public interface IFilterModel
    {
        string Filter { get; set; }
        int? FilterId { get; set; }
    }
}
