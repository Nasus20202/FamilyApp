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
            var model = new BaseViewModel("Join");
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

            return View(model);
        }

        [HttpGet]
        [Route("/join")]
        public IActionResult Join()
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
                }
            }
            return View(model);
        }

        [HttpPost]
        [Route("/join")]
        public IActionResult Join(int code)
        {
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
