﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFExample.Domain.Entities
{
    public class Product: EntityBase
    {
        [MaxLength(250)]
        public string Name { get; set; }
        public int? Stock { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
