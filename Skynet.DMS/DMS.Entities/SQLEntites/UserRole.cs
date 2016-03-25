using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Entities.SQLEntites
{
    public class UserRole : BaseEntity
    {
        public long UserId { get; set; }

        public string RoleIds { get; set; }
    }
}
