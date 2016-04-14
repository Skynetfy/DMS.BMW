using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DMS.Common;
using DMS.WebUI.Models;

namespace DMS.WebUI.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Admin");
            return View();
        }
        [HttpPost]
        public ActionResult Login(string name, string password, string Rbme)
        {
            var message = LoginProvider.UserLogin(name, password);
            if (message.Status == 1)
            {
                if (Rbme != null)
                    CookieHelper.SetCookie("UserName", name, DateTime.Now.AddDays(7));
                return RedirectToAction("Index", "admin");
            }

            return Json(message);
        }

        public async Task<ActionResult> LoginOut()
        {
            await LoginProvider.LoginOut();
            return Redirect("/Login/index");
        }
    }
}