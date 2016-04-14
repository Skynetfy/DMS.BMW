using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMS.Entities.SQLEntites;

namespace DMS.SQLRepository
{
    public interface IChatConnectionRepository : IBaseRepository<ChatConnection>
    {
        List<ChatConnection> GetByUid(long uid);

        int UpdateConnectioned(Int64 uid,string connectionid, bool bl);
    }
}
