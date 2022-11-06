using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFExample.Domain.Entities
{
    public class Category:EntityBase
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        [MaxLength(250)]
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
