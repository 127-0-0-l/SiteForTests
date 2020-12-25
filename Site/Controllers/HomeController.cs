using Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Site.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Dictionary<int, string> tests = DataBase.GetTestNames();

            return View(tests);
        }
    }
}