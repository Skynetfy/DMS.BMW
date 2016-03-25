using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Entities.SQLEntites
{
    public class BaseEntity
    {
        public long Id { get; set; }

        public bool IsDelete { get; set; }

        public DateTime LastModifyDate { get; set; }

        public DateTime CreateDate { get; set; }

        public BaseEntity()
        {
            Id = 0;
            IsDelete = true;
            LastModifyDate = new DateTime(1790, 1, 1);
            CreateDate = new DateTime(1790, 1, 1);
        }
    }
}
