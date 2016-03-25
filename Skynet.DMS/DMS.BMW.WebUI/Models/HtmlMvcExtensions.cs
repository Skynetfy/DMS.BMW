using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DMS.Entities.SQLEntites;

namespace DMS.BMW.WebUI.Models
{
    public static class HtmlMvcExtensions
    {
        public static MvcHtmlString HtmlLeftMenu(this HtmlHelper html, IList<Module> modules)
        {
            var query = modules.Where(x => x.Level == 1).ToList();
            StringBuilder sb = new StringBuilder();
            foreach (var item in query)
            {
                sb.AppendLine(@"<li><div class='link'>");
                sb.AppendLine(@"" + item.ModuleName + "<i class='" + item.Icon + "'></i><i class='fa fa-chevron-down'></i>");
                sb.AppendLine(@"</div>" + BuildChildrenMenu(item.Id, modules.Where(x => x.ParentId == item.Id).ToList()) + " </li>");
            }
            return MvcHtmlString.Create(sb.ToString());
        }

        private static string BuildChildrenMenu(Int64 parentid, IList<Module> modules)
        {
            StringBuilder sb = new StringBuilder();
            if (modules.Count > 0)
            {
                sb.AppendLine(@"<ul class='submenu'>");
                foreach (var item in modules)
                {
                    sb.AppendLine(@"<li><i class='glyphicon glyphicon-paperclip'></i><a href='#' data-href='" + item.Url + "'>" + item.ModuleName + "</a></li>");
                }
                sb.AppendLine(@"</ul>");
            }
            return sb.ToString();
        }
    }
}