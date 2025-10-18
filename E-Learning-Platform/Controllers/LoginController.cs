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
                var user = db.users.Where(x => x.email == u.Identifier).FirstOrDefault();
                if (user != null)
                {
                    if (user.password == u.Password)
                    {
                        Session["Userid"] = user.user_id;
                        Session["Username"] = user.name;
                        Session["Role"] = user.role;
                        Session["profile"] = Profile(user.name);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["msg"] = "<script>alert('Invalid Password')</script>";
                    }
                }
                else
                {
                    TempData["msg"] = "<script>alert('Invalid Mail Id')</script>";
                }

            }
            else
            {
                long mob = Convert.ToInt64(u.Identifier);
                var user = db.users.Where(x => x.phone == mob).FirstOrDefault();
                if (user != null)
                {
                    if (user.password == u.Password)
                    {
                        Session["Userid"] = user.user_id;
                        Session["Username"] = user.name;
                        Session["Role"] = user.role;
                        Session["profile"]= Profile(user.name);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["msg"] = "<script>alert('Invalid Password')</script>";
                    }
                }
                else
                {
                    TempData["msg"] = "<script>alert('Invalid Phone number')</script>";
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
                    user user = new user();
                    user.name = u.Name;
                    user.email = u.Email;
                    user.phone = u.Phone;
                    user.password = u.Password;
                    user.role = u.Role;
                    db.users.Add(user);
                    db.SaveChanges();
                    TempData["UserId"] = user.user_id;
                    TempData["role"] = user.role;
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
            Teacher nteacher= new Teacher();
            nteacher.isActive = false;
            nteacher.qualification = t.Qualification;
            nteacher.subject = t.SubjectExpertise;
            nteacher.gender = t.Gender;
            nteacher.user_id = t.UserId;
            nteacher.department = t.DepartmentId;
            nteacher.Status = "New";
            db.Teachers.Add(nteacher);
            db.SaveChanges();

            return RedirectToAction("Login");
        }
        [HttpPost]
        public ActionResult StudentDetails(DetailsModel s)
        {
            student std=new student();
            std.father_name=s.FatherName;
            std.mother_name=s.MotherName;
            std.gender = s.Gender;
            std.address=s.Address;
            std.course_id=s.CourseId;
            std.user_id=s.UserId;
            db.students.Add(std);
            db.SaveChanges();
            return RedirectToAction("Login");
        }
    }
}