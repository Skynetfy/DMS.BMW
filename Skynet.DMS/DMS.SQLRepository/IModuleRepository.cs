using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMS.Entities.SQLEntites;

namespace DMS.SQLRepository
{
    public interface IModuleRepository : IBaseRepository<Module>
    {
        List<Module> GetListByIds(IList<string> ids);

        List<Module> GetPageList();

        List<Module> GetParentList();

        int Delete(Int64 id);

        Module GetModule(Int64 id);

        Module Update(Module entity);
    }
}
