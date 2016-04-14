using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMS.Entities.SQLEntites;

namespace DMS.SQLRepository
{
    public interface IUserMessageRepository : IBaseRepository<UserMessage>
    {
        int UpdateStatus(Int64 id);

        List<UserMessage> GetMessages(long toUid, int status, MessageTypeEnum type);
    }
}
