using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using DMS.Entities.SQLEntites;
using DMS.Services;

namespace DMS.BMW.WebUI.Controllers
{
    [Authorize]
    public class adminController : Controller
    {
        // GET: admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TopMenu()
        {
            return PartialView();
        }

        public ActionResult LeftMenu()
        {
            var modules = new List<Module>();
            var identity = (ClaimsIdentity)HttpContext.User.Identity;
            var uid = identity.FindFirst(ClaimTypes.Sid).Value;
            if (uid != null)
                modules = UserService.GetModulesByUid(Convert.ToInt64(uid)).ToList();
               //  modules = UserService.GetModulesPageList().ToList();
            return PartialView(modules);
        }

        public ActionResult FooterMenu()
        {
            return PartialView();
        }
    }
}