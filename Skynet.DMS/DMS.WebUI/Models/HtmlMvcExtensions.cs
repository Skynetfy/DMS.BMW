using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DMS.Entities.SQLEntites;

namespace DMS.WebUI.Models
{
    public static class HtmlMvcExtensions
    {
        public static MvcHtmlString HtmlLeftMenu(this HtmlHelper html, IList<Module> modules)
        {
            var query = modules.Where(x => x.Level == 1).ToList();
            StringBuilder sb = new StringBuilder();
            foreach (var item in query)
            {
                sb.AppendLine(@"<li class='treeview'><a href='#'>");
                sb.AppendLine(@"<i class='" + item.Icon + "'></i><span>" + item.ModuleName + "</span><i class='fa fa-angle-left pull-right'></i>");
                sb.AppendLine(@"</a>" + BuildChildrenMenu(item.Id, modules.Where(x => x.ParentId == item.Id).ToList()) + " </li>");
            }
            return MvcHtmlString.Create(sb.ToString());
        }

        private static string BuildChildrenMenu(Int64 parentid, IList<Module> modules)
        {
            StringBuilder sb = new StringBuilder();
            if (modules.Count > 0)
            {
                sb.AppendLine(@"<ul class='treeview-menu menu-open'>");
                foreach (var item in modules)
                {
                    sb.AppendLine(@"<li><a href='#' data-href='" + item.Url + "'><i class='fa fa-circle-o'></i>" + item.ModuleName + "</a></li>");
                }
                sb.AppendLine(@"</ul>");
            }
            return sb.ToString();
        }
    }
}