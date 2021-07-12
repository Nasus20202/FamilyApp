using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyApp
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var model = new BaseViewModel("Family");
            using (var db = new Database())
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = (from c in db.Users
                                where c.Email == User.Identity.Name
                                select c).FirstOrDefault();
                    model.User.Name = user.Name;
                    model.User.Surname = user.Surname;
                    model.User.Email = user.Email;
                    model.User.FamilyId = user.FamilyId;
                    if(user.FamilyId == null)
                    {
                        return View("start", model);
                    }
                }
                else
                {
                    return View("start", model);
                }
            }

            return View(model);
        }

        [HttpGet]
        [Route("/join")]
        public IActionResult Join()
        {
            var model = new BaseViewModel("Join family");
            using (var db = new Database())
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = (from c in db.Users
                                where c.Email == User.Identity.Name
                                select c).FirstOrDefault();
                    model.User.Name = user.Name;
                    model.User.Surname = user.Surname;
                    model.User.Email = user.Email;
                    model.User.FamilyId = user.FamilyId;
                }
                else
                {
                    return Redirect(Url.Action("login", "account"));
                }
            }
            return View(model);
        }

        [HttpPost]
        [Route("/join")]
        public IActionResult Join(string code)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect(Url.Action("login", "account"));
            }
            var model = new BaseViewModel("Join family");
            using (var db = new Database())
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = (from c in db.Users
                                where c.Email == User.Identity.Name
                                select c).FirstOrDefault();
                    model.User.Name = user.Name;
                    model.User.Surname = user.Surname;
                    model.User.Email = user.Email;
                    model.User.FamilyId = user.FamilyId;
                }
            }
            model.Message = "The code is invalid!";
            if (code == null)
            {
                return View(model);
            }
            bool valid = code.Length == 10 ? true : false;
            foreach (char c in code)
            {
                if (c < '0' || c > '9')
                {
                    valid = false; break;
                }
            }
            if (!valid)
            {
                return View(model);
            }
            using (var db = new Database())
            {
                Family family = db.Families.Where(f => f.Code == code).FirstOrDefault();
                if (family == null) {
                    model.Message = "No family is associated with this code!";
                    return View(model);
                }
                User user = db.Users.Where(u => u.Email == User.Identity.Name).FirstOrDefault();
                user.FamilyId = family.FamilyId;
                db.SaveChanges();
            }
            return Redirect("/");
        }

        [Route("/Error/{code:int}")]
        public IActionResult Error(int code)
        {
            var model = new BaseViewModel("Error");
            if (User.Identity.IsAuthenticated)
            {
                using (var db = new Database())
                {
                    var user = (from c in db.Users
                                where c.Email == User.Identity.Name
                                select c).FirstOrDefault();
                    model.User.Name = user.Name;
                    model.User.Surname = user.Surname;
                    model.User.Email = user.Email;
                    model.User.FamilyId = user.FamilyId;
                }
            }
            ViewData["errorCode"] = code;
            ViewData["aboutError"] = "";
            if(code == 404)
            {
                ViewData["aboutError"] = "This page doesn't exist";
            }
            model.Title = "An error occured";
            return View(model);
        }
    }
}
