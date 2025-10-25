using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Learning_Platform.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult AppliedTeacher()
        {
            return View();
        }
    }
}