using E_Learning_Platform.Models;
using E_Learning_Platform.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace E_Learning_Platform.Controllers
{
    public class CourseController : Controller
    {
        /*private readonly object image;*/

        // GET: Course
        e_learning_dbEntities db = new e_learning_dbEntities();

        public ActionResult Course()
        {
         return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Course(CourseDetails model,HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image == null)
                {
                    ModelState.AddModelError("image", "Please upload an image file.");
                    return View(model);
                }

                var obj = new course();
                obj.title = model.Title;
                obj.description = model.Description;
                obj.duretion = model.Duretion;
                obj.course_fee = model.Course_Fee;

                obj.image = UploadImage(image);


                db.courses.Add(obj);
                db.SaveChanges();

                TempData["msg"] = "Course Added Successfully!";
                return RedirectToAction("Index","Course");
            }

            return View(model);
        }
        public ActionResult Index()
        {
            var courses = db.courses.ToList();
            List<CourseDetails> vm = new List<CourseDetails>();
            foreach (var c in courses)
            {
                CourseDetails obj = new CourseDetails()

                {
                    Course_Id = c.course_id,
                    Title = c.title,
                    Description = c.description,
                    Duretion = c.duretion,
                    Course_Fee = c.course_fee,
                    ImagePath = c.image                   
                };
                vm.Add(obj);

            }
            /*var vm=courses.Select(c => new CourseDetails
            {
                Course_Id = c.course_id,
                Title = c.title,
                Description = c.description,
                Duretion = c.duretion,
                Course_Fee = c.course_fee,
                ImagePath = c.image
            }).ToList();*/
            return View(vm);
        }
        public string UploadImage(HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength == 0)
                return null;

            string fileName = Path.GetFileName(file.FileName);
            string folderPath = Server.MapPath("~/Course/");

            // Folder create if not exists
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string fullPath = Path.Combine(folderPath, fileName);
            file.SaveAs(fullPath);

            // Return relative path for database
            return "/Course/" + fileName;
        }
    }
}
