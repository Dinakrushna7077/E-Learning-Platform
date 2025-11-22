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
        public ActionResult Dashboard()
        {
            AdminDashboard adminDash= new AdminDashboard();
            adminDash.TotalStudents = db.users.Where(s=>s.role==1014).Count();
            adminDash.TotalAdmins = db.users.Where(s => s.role == 1012).Count();
            adminDash.TotalTeachers = db.users.Where(s => s.role == 1016).Count();
            adminDash.TotalCourses=db.courses.Count();
            
            return PartialView("_AdminDashboard",adminDash);
        }
        public ActionResult RecentCourse()
        {
            var course=db.courses.OrderByDescending(c=>c.course_id).Take(4).ToList();
            List<RecentCourse> Courselist = new List<RecentCourse>();
            foreach (var c in course)
            {
                Courselist.Add(new RecentCourse
                {
                    CourseTitle=c.title,
                    CourseDescription=c.description,
                    CourseFee=c.course_fee.Value,
                    Duration=c.duretion,
                    Instructor="- - -"
                });
            }
            return PartialView("_RecentCourse",Courselist);
        }
        public ActionResult RecentUser()
        {
            // Step 1: Get recent unique user IDs (top 3)
            var userIds = db.Logs
                .OrderByDescending(l => l.LogId)
                .GroupBy(l => l.UserId)
                .Select(g => g.FirstOrDefault().UserId)
                .Take(3)
                .ToList();

            List<RecentUserVM> userList = new List<RecentUserVM>();

            foreach (var id in userIds)
            {
                var user = db.users.FirstOrDefault(u => u.user_id == id);

                if (user != null)
                {
                    userList.Add(new RecentUserVM
                    {
                        Name = user.name,
                        Role = user.role
                    });
                }
            }

            return PartialView("_RecentUser", userList);
        }

        public ActionResult AdminRegistration()
        {
            List<string> designation = new List<string> { "Content Admin", "System Administrator", "Assessment Admin", "Finance Admin", "Analytics Admin", "Communication Admin", "Super Admin", "Instructor Coordinator", "Library Admin" };
            ViewBag.designation = new SelectList(designation);
            return View();

       
        }
        [HttpPost]
        public ActionResult AdminRegistration(AdminRegister ar)
        {
            List<string> designation = new List<string> { "Content Admin", "System Administrator", "Assessment Admin", "Finance Admin", "Analytics Admin", "Communication Admin", "Super Admin", "Instructor Coordinator", "Library Admin" };
            ViewBag.designation = new SelectList(designation);
            if (ModelState.IsValid)
            {
                if (ar.Password == ar.ConfirmPassword)
                {
                    /*     //user u = new user();
                         //u.name = ar.Name;
                         //u.email = ar.Email;
                         //u.phone = ar.Mobile;
                         //u.role = 1014;
                         //DateTime now = DateTime.Now;
                         //db.users.Add(u);
                         //db.SaveChanges();
                         //Admin ad = new Admin();
                         //ad.desig = ar.Designation;
                         //db.Admins.Add(ad);
                         //db.SaveChanges(); */

                    var user=db.NewUser(ar.Name, ar.Email, ar.Password, ar.Mobile, 1014).FirstOrDefault();
                    var data = db.NewAdmin(ar.Designation, user.UserId);
                         TempData["msg"] = "<script>alert('Registration Successful')</script>";
                    return RedirectToAction("Login", "Login");



                }
                else
                {
                    TempData["msg"] = "<script>alert('Password and Confirm Password do not match')</script>";
                }
            }
            return View(ar);
        }

    }

}