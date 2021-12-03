using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Models
{
    public class ModelPaging
    {
        public ModelPaging()
        {
            PageSize = 10;
            Page = 1;
        }
        public int PageSize { get; set; }
        public int Page { get; set; }
    }
    
}
