using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_Learning_Platform.Models;
using E_Learning_Platform.Models.ViewModel;

namespace E_Learning_Platform.Controllers
{
              public class AdminController : Controller
    {
        // GET: Admin
        e_learning_dbEntities db = new e_learning_dbEntities();
        public ActionResult AppliedTeacher()
        {
            var teachers = db.Teachers.Where(t=> t.Status=="New").ToList();
            List<AppliedTeacher> atList = new List<AppliedTeacher>();
            foreach (var teacher in teachers)
            {
                AppliedTeacher at = new AppliedTeacher()
                {
                    AppliedDate = (DateTime)teacher.hire_date,
                    Name = GetTeachertsName(teacher.user.user_id),
                    Gmail = GetTeachertsGmail(teacher.user.user_id),
                    Qualification = teacher.qualification,
                    Subject = teacher.subject
                };
                atList.Add(at);
            }
            return View(atList);
        }
        private string GetTeachertsName(int userId)
        {
            var user = db.users.Where(x => x.user_id == userId).FirstOrDefault();
            return user.name;
        }
        private string GetTeachertsGmail(int userId)
        {
            var user = db.users.Where(x => x.user_id == userId).FirstOrDefault();
            return user.email;
        }

        //please check the bellow code 
        public ActionResult ApproveTeacher(int id)
        {
            var teacher = db.Teachers.Where(t => t.user_id == id).FirstOrDefault();
            if (teacher != null)
            {
                teacher.Status = "Approved";
                teacher.isActive = true;
                db.SaveChanges();
            }
            return RedirectToAction("AppliedTeacher");
        }


        public ActionResult RejectTeacher(int id)
        {
            var teacher = db.Teachers.Where(t => t.user_id == id).FirstOrDefault();
            if (teacher != null)
            {
                teacher.Status = "Rejected";
                teacher.isActive = false;
                db.SaveChanges();
            }
            return RedirectToAction("AppliedTeacher");
        }

        public ActionResult AdminDashBoard()
        {
                       return View();
        }
    }
                            
}