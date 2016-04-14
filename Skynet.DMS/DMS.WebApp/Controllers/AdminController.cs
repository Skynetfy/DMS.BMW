using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using DMS.Entities.SQLEntites;
using DMS.Services;

namespace DMS.WebApp.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Top()
        {
            var identity = (ClaimsIdentity)HttpContext.User.Identity;
            var uid = Convert.ToInt32(identity.FindFirst(ClaimTypes.Sid).Value);
            var user = UserService.GetUserById(uid);
            ViewBag.UserName = user.UserName;
            return PartialView();
        }

        public ActionResult Left()
        {
            var identity = (ClaimsIdentity)HttpContext.User.Identity;
            var uid = Convert.ToInt32(identity.FindFirst(ClaimTypes.Sid).Value);
            var user = UserService.GetUserById(uid);
            var modules = new List<Module>();
            if (uid == 1)
                modules = UserService.GetModulesPageList().ToList();
            else
            {
                modules = UserService.GetModulesByUid(Convert.ToInt64(uid)).ToList();
            }

            ViewBag.UserName = user.UserName;
            return PartialView(modules);
        }

        public ActionResult Content()
        {
            return PartialView();
        }

        public ActionResult Footer()
        {
            return PartialView();
        }
    }
}