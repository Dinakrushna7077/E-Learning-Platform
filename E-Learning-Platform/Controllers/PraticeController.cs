using E_Learning_Platform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Learning_Platform.Controllers
{
    public class PraticeController : Controller
    {
        // GET: Pratice
        e_learning_dbEntities db = new e_learning_dbEntities();
        public ActionResult Index()
        {
            var courses = db.courses.ToList();
            return View(courses);
        }
        public ActionResult Details(int id)
        {
            var course = db.courses.Find(id);
            return View(course);
        }

    }
}