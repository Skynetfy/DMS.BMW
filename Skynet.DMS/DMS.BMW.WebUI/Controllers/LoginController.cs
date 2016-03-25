using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DMS.BMW.WebUI.Models;

namespace DMS.BMW.WebUI.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "admin");
            return View();
        }

        [HttpPost]
        public ActionResult Login(string name, string password)
        {
            var message = LoginProvider.UserLogin(name, password);
            if (message.Status == 0)
                return RedirectToAction("Index", "admin");
            return Json(message);
        }

        public async Task<ActionResult> LoginOut()
        {
            await LoginProvider.LoginOut();
            return Redirect("/Login/index");
        }
    }
}