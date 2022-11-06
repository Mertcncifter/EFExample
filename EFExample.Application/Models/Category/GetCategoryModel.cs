using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFExample.Application.Models.Category
{
    public class GetCategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StatusType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
