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
    [Authorize]
    public class HobbiesController : Controller
    {
        // GET: Hobbies
        public ActionResult Index()
        {
            var dataList = HobbitiesService.GetHobbitesesList();
            return View(dataList);
        }

        public ActionResult GetHobbitiesPageList(string search = "")
        {
            var count = HobbitiesService.GetHobbitiesPageCount(search);
            var datalist = HobbitiesService.GetHobbitesesList();
            var pagerList = new BtpTableDataPage<Hobbites>();
            pagerList.total = count;
            pagerList.rows = datalist;
            return new JsonNetResult()
            {
                Data = pagerList,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult GetHobbitiesTypePageList(string search = "")
        {
            var count = HobbitiesService.GetHobbityTypePageCount(search);
            var datalist = HobbitiesService.GetHobbityTypesList();
            var pagerList = new BtpTableDataPage<HobbityType>();
            pagerList.total = count;
            pagerList.rows = datalist;
            return new JsonNetResult()
            {
                Data = pagerList,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult AddHobbities(string hName, int typeId = 0, string imageUrl = "", string hbDesc = "")
        {
            var entity = new Hobbites()
            {
                TypeId = typeId,
                HbName = hName.Trim(),
                ImageUrl = imageUrl.Trim(),
                HbDesc = hbDesc.Trim()
            };
            HobbitiesService.AddHobbites(entity);
            return Json(new JsonResponseResult<string>()
            {
                Status = 1,
                Message = "Success"
            });
        }

        public ActionResult AddHobbitType(string tName, string thbDesc = "")
        {
            var entity = new HobbityType()
            {
                TName = tName.Trim(),
                HbDesc = thbDesc.Trim()
            };
            HobbitiesService.AddHobbityType(entity);
            return Json(new JsonResponseResult<string>()
            {
                Status = 1,
                Message = "Success"
            });
        }

        public ActionResult HobbitiesList()
        {
            var dataList = HobbitiesService.GetHobbitesesList();
            return PartialView(dataList);
        }

        public ActionResult Calender()
        {
            return View();
        }
    }
}