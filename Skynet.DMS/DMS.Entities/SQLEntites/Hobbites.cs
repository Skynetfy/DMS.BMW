using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Entities.SQLEntites
{
    public class Hobbites : BaseEntity
    {
        public int TypeId { get; set; }

        public string HbName { get; set; }

        public string HbDesc { get; set; }

        public string ImageUrl { get; set; }
    }
}
