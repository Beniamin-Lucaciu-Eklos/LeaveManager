using Microsoft.AspNetCore.Mvc;
using LeaveManager.Web.Models;

namespace LeaveManager.Web.Controllers
{
    public class TestController : Controller
    {
        // GET: TestController
        public ActionResult Index()
        {
            var vmTest = new TestViewModel
            {
                Name = "Student",
                DateOfBirth = DateTime.Now
            };
            return View(vmTest);
        }

    }
}
