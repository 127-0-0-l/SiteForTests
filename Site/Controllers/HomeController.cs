using Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

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

        public ActionResult AddTest()
        {
            return View();
        }

        public ActionResult EditTest(int testId)
        {
            return View(DataBase.GetQuestions(testId));
        }

        public void RemoveTest(int testId)
        {

        }

        [HttpPost]
        public ActionResult ProcessData(string jsonString)
        {
            JavaScriptSerializer jSerialize = new JavaScriptSerializer();
            Test test = jSerialize.Deserialize<Test>(jsonString);

            DataBase.AddTest(test);

            return Json(new { status = 1, message = "test created" });
        }
    }
}