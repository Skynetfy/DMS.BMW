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
                sb.AppendLine(@"<li class='treeview'>");
                sb.AppendLine(@"<a href='#'><i class='" + item.Icon + "'></i> <span>" + item.ModuleName + "</span>");
                sb.AppendLine(@"<i class='fa fa-angle-left pull-right'></i></a>");
                sb.AppendLine(BuildChildrenMenu(item.Id, query));
                sb.AppendLine(@"</li>");
            }
            return MvcHtmlString.Create(sb.ToString());
        }

        private static string BuildChildrenMenu(Int64 parentid, IList<Module> modules)
        {
            StringBuilder sb = new StringBuilder();
            var dataList = modules.Where(x => x.ParentId == parentid).ToList();
            if (dataList.Count > 0)
            {
                sb.AppendLine(@"<ul class='treeview-menu'>");
                foreach (var item in dataList)
                {
                    sb.AppendLine(@"<li><a href='#' data-url='" + item.Url + "'><i class='fa fa-circle-o'></i>" + item.ModuleName + "</a>"+ BuildChildrenMenu(item.Id, modules) + "</li>");
                }
                sb.AppendLine(@"</ul>");
            }
            return sb.ToString();
        }
    }
}