using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Learning_Platform.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult StudentDashboard()
        {
            return View();
        }
        public ActionResult AvailableCourses()
        {
            return PartialView("_AvailableCourse");
        }
        public ActionResult MyCourse()
        {
            return PartialView("_MyCourse");
        }
        public ActionResult Assignment()
        {
            return PartialView("_Assignment");
        }
        public ActionResult Progress()
        {
            return PartialView("_Progress");
        }
        public ActionResult Resources()
        {
            return PartialView("_Resources");
        }
        public ActionResult LiveEvent()
        {
            return PartialView("_LiveEvent");
        }
        public ActionResult Badges()
        {
            return PartialView("_Badges");
        }

        public ActionResult Dashboard3()
        {
            return View();
        }
    }
}