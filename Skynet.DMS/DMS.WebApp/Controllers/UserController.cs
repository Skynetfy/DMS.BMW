using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DMS.Common;
using DMS.Entities.SQLEntites;
using DMS.Services;
using DMS.WebUI.Models;

namespace DMS.WebApp.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Module()
        {
            var moduleModel = UserService.GetParentModulesList();
            return View(moduleModel);
        }

        public JsonResult GetUserPageList(string search = "")
        {
            var count = 0;
            var datalist = UserService.GetUserPageList(search, out count);
            var pagerList = new BtpTableDataPage<User>();
            pagerList.total = count;
            pagerList.rows = datalist;
            return new JsonNetResult()
            {
                Data = pagerList,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult GetModulesPageList(string search)
        {
            var dataList = UserService.GetModulesPageList();
            var pagerList = new BtpTableDataPage<Module>();
            pagerList.total = dataList.Count;
            pagerList.rows = dataList;
            return new JsonNetResult()
            {
                Data = pagerList,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult GetModuleZtreeDataList(int id)
        {
            var idStr = UserService.GetRoleById(id).MenuIds;
            var moduleids = string.IsNullOrEmpty(idStr) ? new string[0] : idStr.Split(',');
            var dataList = UserService.GetModuleTreeDataList(moduleids);
            return new JsonNetResult(dataList);
        }

        [HttpPost]
        public ActionResult EditUserRole(int uid = 0, string roleids = "")
        {
            var urid = UserService.ExsiteUserRole(uid);
            if (urid > 0)
            {
                var userRole = new UserRole()
                {
                    Id = urid,
                    UserId = uid,
                    RoleIds = roleids.Trim()
                };
                UserService.UpdateUserRole(userRole);
            }
            else
            {
                var userRole = new UserRole()
                {
                    UserId = uid,
                    RoleIds = roleids.Trim()
                };
                UserService.AddUserRole(userRole);
            }
            return Json(new JsonResponseResult<string>()
            {
                Status = 0,
                Message = "Success"
            });
        }

        public ActionResult GetRolesZtreeDataList(int id)
        {
            var dataList = UserService.GetTreeRolesByUid(id);
            return new JsonNetResult(dataList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddModules(string menuname, int level = 1, string url = "", string iconimage = "",
            Int64 parentid = 0, bool status = true, string moduleid = "0")
        {
            var module = new Module();
            if (string.IsNullOrEmpty(menuname))
                return Json(new JsonResponseResult<string>()
                {
                    Status = -1,
                    Message = "菜单名称为空"
                });

            module.ModuleName = menuname.Trim();
            module.Url = url.Trim();
            module.ParentId = parentid;
            module.Icon = iconimage;
            module.IsUse = status;
            module.Level = level;
            if (moduleid.Equals("0"))
                UserService.AddModule(module);
            else
            {
                module.Id = Convert.ToInt64(moduleid);
                UserService.UpdateModule(module);
            }
            return Json(new JsonResponseResult<string>()
            {
                Status = 0,
                Message = "Success"
            });
        }

        public ActionResult RoleIndex()
        {
            return View();
        }

        public ActionResult GetRolePageList(string search)
        {
            var count = 0;
            var datalist = UserService.GetRolePageList(search, out count);
            var pagerList = new BtpTableDataPage<Role>();
            pagerList.total = count;
            pagerList.rows = datalist;
            return new JsonNetResult()
            {
                Data = pagerList,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        [HttpPost]
        public ActionResult AddUser(string username, string displayname, string userpassword,
            string email, string phoneno, int status)
        {
            var user = new User()
            {
                UserName = username,
                DisplayName = displayname,
                Password = userpassword,
                Email = email,
                PhoneNo = phoneno,
                Status = status
            };
            UserService.AddUser(user);
            return Json(new JsonResponseResult<string>()
            {
                Status = 0,
                Message = "Success"
            });
        }
        [HttpPost]
        public ActionResult AddRoles(string rolename = "", string moduleids = "", string roleid = "0")
        {
            var role = new Role();
            role.RoleName = rolename;
            role.MenuIds = moduleids;
            if (roleid.Equals("0"))
                UserService.AddRole(role);
            else
            {
                role.Id = Convert.ToInt64(roleid);
                UserService.UpdateRole(role);
            }
            return Json(new JsonResponseResult<string>()
            {
                Status = 0,
                Message = "Success"
            });
        }

        [HttpPost]
        public ActionResult DeleteRole(string id = "")
        {
            var list = new List<int>();
            var ids = id.Split(',');
            foreach (var e in ids)
            {
                if (!string.IsNullOrEmpty(e))
                    list.Add(Convert.ToInt32(e));
            }
            UserService.DeleteRoleById(list);
            return Json(new JsonResponseResult<string>()
            {
                Status = 0,
                Message = "Success"
            });
        }

        [HttpPost]
        public ActionResult DeleteUser(string id = "")
        {
            var list = new List<int>();
            var ids = id.Split(',');
            foreach (var e in ids)
            {
                if (!string.IsNullOrEmpty(e))
                    list.Add(Convert.ToInt32(e));
            }
            UserService.DeleteUserById(list);
            return Json(new JsonResponseResult<string>()
            {
                Status = 0,
                Message = "Success"
            });
        }

        [HttpPost]
        public ActionResult DeleteModule(string id = "")
        {
            var list = new List<int>();
            var ids = id.Split(',');
            foreach (var e in ids)
            {
                if (!string.IsNullOrEmpty(e))
                    list.Add(Convert.ToInt32(e));
            }
            UserService.DeleteModuleById(list);
            return Json(new JsonResponseResult<string>()
            {
                Status = 0,
                Message = "Success"
            });
        }

        public ActionResult GetUserById(int id)
        {
            return Json(new JsonResponseResult<User>()
            {
                Status = 0,
                Message = "Success",
                Result = UserService.GetUserById(id)
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetRoleById(int id)
        {
            return Json(new JsonResponseResult<Role>()
            {
                Status = 0,
                Message = "Success",
                Result = UserService.GetRoleById(id)
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetModuleById(int id)
        {
            return Json(new JsonResponseResult<Module>()
            {
                Status = 0,
                Message = "Success",
                Result = UserService.GetModuleById(id)
            }, JsonRequestBehavior.AllowGet);
        }
    }
}