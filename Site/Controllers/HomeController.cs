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
            var tests = DataBase.GetTestNames();

            return View(tests);
        }

        public ActionResult Test(int testId = 1)
        {
            var test = DataBase.GetQuestions(testId);

            return View(test);
        }
    }
}