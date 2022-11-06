using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFExample.Application.Models.Product
{
    public class UpdateProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Stock { get; set; }
        public int CategoryId { get; set; }
        public int StatusType { get; set; }
    }
}
