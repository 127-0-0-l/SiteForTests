using Site.Models;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Site.Controllers
{
    public class HomeController : Controller
    {
        // Home page.
        public ActionResult Index()
        {
            var tests = DataBase.GetTestNames();

            return View(tests);
        }

        // Page for testing.
        public ActionResult Test(int testId = 1)
        {
            var questions = DataBase.GetQuestions(testId);

            return View(questions);
        }

        // Page for add new test.
        public ActionResult AddTest()
        {
            return View();
        }

        // Action without view to add new test to database.
        [HttpPost]
        public ActionResult AddTest(string jsonString)
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            Test test = jsSerializer.Deserialize<Test>(jsonString);

            DataBase.AddTest(test);

            return Json(new { status = 1, message = "test created" });
        }

        // Action without view to delete test.
        public ActionResult DeleteTest(int testId)
        {
            DataBase.DeleteTest(testId);

            return RedirectToAction("Index");
        }
    }
}