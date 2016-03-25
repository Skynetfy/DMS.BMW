using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMS.SQLRepository.Repository;

namespace DMS.SQLRepository
{
    public class DbRepositoryFactory
    {
        public static IUserRepository DbUserRepository
        {
            get { return new UserRepository(); }
        }

        public static IUserRoleRepository DbUserRoleRepository
        {
            get { return new UserRoleRepository(); }
        }

        public static IRoleRepository DbRoleRepository
        {
            get { return new RoleRepository(); }
        }

        public static IModuleRepository DbModuleRepository
        {
            get { return new ModuleRepository(); }
        }
    }
}
