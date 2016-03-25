using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMS.Entities.SQLEntites;

namespace DMS.SQLRepository
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        List<Role> GetListByIds(IList<string> ids);

        List<Role> GetPageList(string search);

        int GetPageCount(string search);

        int Delete(Int64 id);

        Role GetRole(Int64 id);

        Role Update(Role entity);
    }
}
