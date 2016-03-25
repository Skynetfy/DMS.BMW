using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMS.Entities.SQLEntites;

namespace DMS.SQLRepository
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        T Add(T entity);
    }
}
