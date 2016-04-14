using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Entities.SQLEntites
{
    public class ChatConnection: BaseEntity
    {
        public string ConnectionId { get; set; }

        public long Uid { get; set; }

        public string UserAgent { get; set; }

        public bool Connected { get; set; }
    }
}
