using E_Learning_Platform.Models;
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

        public ActionResult Regiseter()
        {

            return View();
        }
    }
}