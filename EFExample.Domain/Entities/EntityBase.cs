using EFExample.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFExample.Domain.Entities
{
    public class EntityBase
    {
        public int Id { get; set; }
        public StatusType Statu { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public EntityBase()
        {
            Statu = StatusType.Active;
            CreatedDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
        }
    }
}
