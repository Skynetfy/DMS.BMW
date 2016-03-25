using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Threading;
using System.Threading.Tasks;
using DMS.Common;
using DMS.Services;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace DMS.BMW.WebUI.Models
{
    public class LoginProvider
    {
        private static IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.Current.GetOwinContext().Authentication; }
        }

        public static JsonResponseResult<string> UserLogin(string username, string password)
        {
            var result = new JsonResponseResult<string>();
            try
            {
                if (UserService.CheckUser(username))
                {
                    var user = UserService.CheckUserLogin(username, password);
                    if (user == null)
                    {
                        result.Status = -1;
                        result.Message = "密码错误";
                    }
                    else
                    {
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Sid, user.Id.ToString()));
                        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                        claims.Add(new Claim(ClaimTypes.Email, user.Email));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.DisplayName));
                        if (user.DisplayName.Equals("admin"))
                            claims.Add(new Claim(ClaimTypes.Role, "admin"));
                        var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                        //var principal = new ClaimsPrincipal(identity);
                        AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                        AuthenticationManager.SignIn(new AuthenticationProperties()
                        {
                            IsPersistent = true,
                            RedirectUri = "/Admin/Index",
                            AllowRefresh = true
                        }, identity);
                    }
                }
                else
                {
                    result.Status = -1;
                    result.Message = "用户不存在";
                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public static async Task LoginOut()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }
    }
}