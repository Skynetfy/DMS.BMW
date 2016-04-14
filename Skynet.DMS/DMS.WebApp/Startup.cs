using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(DMS.WebApp.Startup))]

namespace DMS.WebApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                //CookieDomain = "./",
                LoginPath = new PathString("/Login/index"),
                LogoutPath = new PathString("/Login/LoginOut"),
                CookieSecure = CookieSecureOption.Never,
                ExpireTimeSpan = TimeSpan.FromHours(1)
            });
            app.MapSignalR();
        }
    }
}
