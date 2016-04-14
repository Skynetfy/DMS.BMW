using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMS.Entities.SQLEntites;

namespace DMS.SQLRepository
{
    public interface IChatUserRepository: IBaseRepository<ChatUser>
    {
        ChatUser GetByName(string userName);
    }
}
