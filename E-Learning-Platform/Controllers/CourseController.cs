using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_Learning_Platform.Models;
using System.Data.Entity;

namespace E_Learning_Platform.Controllers
{
    public class CourseController : Controller
    {
        e_learning_dbEntities db = new e_learning_dbEntities();

        public ActionResult Home()
        {
            return View();
        }

        public ActionResult ViewCourse()
        {
            var courses = db.courses.ToList();
            return PartialView("_Course",courses);
        }

        public ActionResult AddCourse()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCourse(course e, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                // handle uploaded image
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    var allowed = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var ext = System.IO.Path.GetExtension(imageFile.FileName)?.ToLowerInvariant();
                    if (!allowed.Contains(ext))
                    {
                        ModelState.AddModelError("image", "Only JPG/PNG/GIF images are allowed.");
                        return View(e);
                    }

                    var fileName = Guid.NewGuid().ToString() + ext;
                    var relativePath = "/Uploads/Courses/" + fileName;
                    var physicalPath = Server.MapPath(relativePath);

                    var dir = System.IO.Path.GetDirectoryName(physicalPath);
                    if (!System.IO.Directory.Exists(dir))
                        System.IO.Directory.CreateDirectory(dir);

                    imageFile.SaveAs(physicalPath);
                    e.image = relativePath; // store relative path in DB
                }

                e.created_at = DateTime.Now;
                db.courses.Add(e);
                db.SaveChanges();
                return RedirectToAction("Home");
            }
            return View(e);
        }
    }
}