using E_Learning_Platform.Models;
using E_Learning_Platform.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace E_Learning_Platform.Controllers
{
    public class LoginController : Controller
    {
        e_learning_dbEntities db = new e_learning_dbEntities();
        // GET: Login

        public ActionResult Login()
        {

            LoginModel u = new LoginModel();

            return View(u);

        }
        [HttpPost]
        public ActionResult Login(LoginModel u)
        {
            if (IsValidEmail(u.Identifier))
            {
                var user = db.Login1(u.Identifier,null,u.Password).FirstOrDefault();
                if (user != null)
                {
                    Session["Userid"] = user.UserId;
                    Session["Username"] = user.UserName;
                    Session["Role"] = user.RoleId;
                    Session["profile"] = Profile(user.UserName);
                    TrackLogins(user.UserId);
                    if (user.RoleId == 1012)
                    {
                        return RedirectToAction("AdminDashBoard", "Admin");

                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["msg"] = "<script>alert('Invalid Mail Id Or Password')</script>";
                }

            }
            else
            {
                long mob = Convert.ToInt64(u.Identifier);
                var user = db.Login1(null,mob,u.Password).FirstOrDefault();
                if(user!=null)
                {
                    Session["Userid"] = user.UserId;
                    Session["Username"] = user.UserName;
                    Session["Role"] = user.RoleId;
                    Session["profile"] = Profile(user.UserName);
                    TrackLogins(user.UserId);
                    if (user.RoleId == 1012)
                    {
                        return RedirectToAction("AdminDashBoard", "Admin");
                    }
                    return RedirectToAction("Index", "Home");
                }                
                else
                {
                    TempData["msg"] = "<script>alert('Invalid Phone number Or Password')</script>";
                }

            }

            return View(u);

        }

        public static bool IsValidEmail(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            // Basic email regex pattern
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            return Regex.IsMatch(input, emailPattern, RegexOptions.IgnoreCase);
        }
        
        private string Profile(string name)
        {
          var namepart=name.Split(' ');
            string profile=namepart[0].Substring(0,1).ToUpper();
            if(namepart.Length>=2)
            {
                profile=profile+namepart[namepart.Length-1].Substring(0,1).ToUpper();
            }
            return profile;
        }

        public ActionResult Register()
        {
            UserModel model = new UserModel();
            List<Role> roles = db.Roles.ToList();
            roles.Remove(roles.Where(a => a.role_id == 1012).FirstOrDefault());
            ViewBag.roles = new SelectList(roles, "role_id", "role_name");
            return View(model);
        }
        [HttpPost]
        public ActionResult Register(UserModel u)
        {
            List<Role> roles = db.Roles.ToList();
            roles.Remove(roles.Where(a => a.role_id == 1012).FirstOrDefault());
            ViewBag.roles = new SelectList(roles, "role_id", "role_name");
            if (ModelState.IsValid)
            {
                if(u.Password==u.ConfirmPassword)
                {
                    var new_user=db.NewUser(u.Name, u.Email, u.Password,u.Phone, u.Role).FirstOrDefault();
                    TempData["UserId"] = new_user.UserId;
                    TempData["role"] = new_user.RoleId;
                    TempData["msg"] = "<script>alert('Registration Successful')</script>";
                    return RedirectToAction("Details");
                }
                else
                {
                    TempData["msg"] = "<script>alert('Password and Confirm Password do not match')</script>";
                }
            }
            return View(u);
        }
        public ActionResult Details()
        {
            List<course> courses=db.courses.ToList();
            ViewBag.courses = new SelectList(courses,"course_id","title");

            //define qualifications list
            List<string> qualifications = new List<string>
            {
                "Bachelor's Degree",
                "Master's Degree",
                "Doctorate (Ph.D.)",
                "Professional Certification"
            };
            ViewBag.qualifications = new SelectList(qualifications);
            var dept=db.Departments.ToList();
            ViewBag.department = new SelectList(dept, "dept_id", "dept_name");
            DetailsModel t = new DetailsModel();
            t.UserId = Convert.ToInt32(TempData["UserId"]);
            return View(t);
        }
        [HttpPost]
        public ActionResult TeacherDetails(DetailsModel t)
        {
            db.NewTeacher(t.Qualification, t.SubjectExpertise,t.Gender,false,t.DepartmentId,t.UserId);
            return RedirectToAction("Login");
        }
        [HttpPost]
        public ActionResult StudentDetails(DetailsModel s)
        {
            db.NewStudent(s.FatherName,s.MotherName,s.Gender,s.Address,null,s.UserId);
            return RedirectToAction("Login");
        }
        private void TrackLogins(int id)
        {
            Log log = new Log();
            log.UserId = id;
            log.Timestamp = DateTime.Now;
            db.Logs.Add(log);
            db.SaveChanges();

        }
    }

}