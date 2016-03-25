using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMS.Common;
using DMS.Entities.SQLEntites;
using DMS.SQLRepository;

namespace DMS.Services
{
    public class UserService
    {
        private static IUserRepository dbUserRepository = DbRepositoryFactory.DbUserRepository;
        private static IRoleRepository dbRoleRepository = DbRepositoryFactory.DbRoleRepository;
        private static IModuleRepository dbModuleRepository = DbRepositoryFactory.DbModuleRepository;
        private static IUserRoleRepository dbUserRoleRepository = DbRepositoryFactory.DbUserRoleRepository;

        public static bool CheckUser(string userName)
        {
            return dbUserRepository.Existe(userName);
        }
        public static User CheckUserLogin(string name, string password)
        {
            return dbUserRepository.CheckUserLogin(name, password);
        }
        public static void AddModule(Module entity)
        {
            dbModuleRepository.Add(entity);
        }
        public static IList<Module> GetModulesByUid(long uid)
        {
            var list = new List<Module>();
            var userRole = dbUserRoleRepository.GetByUid(uid);
            if (userRole != null)
            {
                var roleid = userRole.RoleIds.Split(',').ToList();
                var roles = dbRoleRepository.GetListByIds(roleid);
                var moduleids = roles.Select(x => x.MenuIds).ToList();
                list = dbModuleRepository.GetListByIds(moduleids);
            }
            return list;
        }
        public static IList<Module> GetModulesPageList()
        {
            return dbModuleRepository.GetPageList();
        }
        public static List<Module> GetParentModulesList()
        {
            return dbModuleRepository.GetParentList();
        }
        public static List<ZtreeDataNodes> GetModuleTreeDataList(params string[] ids)
        {
            var dataList = dbModuleRepository.GetPageList();
            var jsonTreeData = new List<ZtreeDataNodes>();
            foreach (var item in dataList)
            {
                jsonTreeData.Add(new ZtreeDataNodes()
                {
                    id = (int)item.Id,
                    pId = (int)item.ParentId,
                    name = item.ModuleName,
                    iconSkin = item.ParentId == 0 ? "diy02" : "diy01",
                    Checked = ids.Contains(item.Id.ToString())
                });
            }
            return jsonTreeData;
        }
        public static void UpdateModule(Module entity)
        {
            dbModuleRepository.Update(entity);
        }
        public static void AddUser(User entity)
        {
            dbUserRepository.Add(entity);
        }
        public static void AddRole(Role entity)
        {
            dbRoleRepository.Add(entity);
        }
        public static List<User> GetUserPageList(string search, out int count)
        {
            count = dbUserRepository.GetPageCount(search);
            return dbUserRepository.GetPageList(search).ToList();
        }
        public static List<Role> GetRolePageList(string search, out int count)
        {
            count = dbRoleRepository.GetPageCount(search);
            return dbRoleRepository.GetPageList(search).ToList();
        }
        public static void DeleteRoleById(List<int> ids)
        {
            foreach (var id in ids)
            {
                dbRoleRepository.Delete(id);
            }
        }
        public static void DeleteUserById(List<int> ids)
        {
            foreach (var id in ids)
            {
                dbUserRepository.Delete(id);
            }
        }
        public static void DeleteModuleById(List<int> ids)
        {
            foreach (var id in ids)
            {
                dbModuleRepository.Delete(id);
            }
        }
        public static User GetUserById(int id)
        {
            return dbUserRepository.GetUser(id);
        }
        public static Role GetRoleById(int id)
        {
            return dbRoleRepository.GetRole(id);
        }
        public static Module GetModuleById(int id)
        {
            return dbModuleRepository.GetModule(id);
        }
        public static List<ZtreeDataNodes> GetTreeRolesByUid(int id)
        {
            var roles = new List<Role>();
            var userRole = dbUserRoleRepository.GetByUid(id);
            if (userRole != null)
            {
                List<string> ids = userRole.RoleIds.Split(',').ToList();
                roles = dbRoleRepository.GetListByIds(ids);
            }
            var ids_ = roles.Select(x => x.Id).ToList();
            var dataList = dbRoleRepository.GetPageList("");
            var jsonTreeData = new List<ZtreeDataNodes>();
            foreach (var item in dataList)
            {
                jsonTreeData.Add(new ZtreeDataNodes()
                {
                    id = (int)item.Id,
                    pId = 0,
                    name = item.RoleName,
                    iconSkin = "diy02",
                    Checked = ids_.Contains(item.Id)
                });
            }
            return jsonTreeData;
        }
        public static void UpdateRole(Role entity)
        {
            dbRoleRepository.Update(entity);
        }
        public static long ExsiteUserRole(long uid)
        {
            return dbUserRoleRepository.Exsite(uid);
        }
        public static int UpdateUserRole(UserRole entity)
        {
            return dbUserRoleRepository.Update(entity);
        }
        public static void AddUserRole(UserRole entity)
        {
            dbUserRoleRepository.Add(entity);
        }
    }
}
