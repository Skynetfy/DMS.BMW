using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMS.Entities.SQLEntites;

namespace DMS.SQLRepository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        bool Existe(string name);

        int Delete(Int64 id);

        User CheckUserLogin(string name, string password);

        IList<User> GetPageList(string search);

        int GetPageCount(string search);

        User GetUser(Int64 id);

        User GetUser(string username);
    }
}
