using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MODALobj;
using BL;

namespace Fstr.Areas.ControlPanel.Controllers
{
    public class HomeController : Controller
    {
        // GET: ControlPanel/Home
        public ActionResult Index()

        {
            List<CheckOrd> Ord_list = new List<CheckOrd>();
            Ord_list = Blproduct.GetCheckOrd();
            return View(Ord_list);
        }
        public ActionResult NewOrders()
        {
            List<CheckOrd> Ord_list = new List<CheckOrd>();
            Ord_list = Blproduct.GetCheckOrd();
            var list = (from ord in Ord_list
                        where ord.Status == "Not Delevered"
                        select ord).ToList();

            return View(list);
        }

        public ActionResult test()
        {
            return View();
        }
    }
}