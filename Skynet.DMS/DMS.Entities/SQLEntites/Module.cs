using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Entities.SQLEntites
{
    public class Module:BaseEntity
    {
        public string ModuleName { get; set; }

        public string Url { get; set; }

        public string Icon { get; set; }

        public long ParentId { get; set; }

        public bool IsUse { get; set; }

        public int Level { get; set; }

        public int OrderBy { get; set; }
    }
}
