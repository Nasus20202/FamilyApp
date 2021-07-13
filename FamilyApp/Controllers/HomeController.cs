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
            var model = new BaseViewModel("Home");
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
                    if (user.FamilyId == null)
                    {
                        return View("start", model);
                    }
                }
                else
                {
                    return View("start", model);
                }

                return Redirect("/family");
            }
        }

        [HttpGet]
        [Route("/create")]
        public IActionResult Create()
        {
            var model = new FamilyModel("Create");
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
                    return Redirect(Url.Action("login", "account")+"?ReturnUrl=/create");
                }
            }
            return View(model);
        }

        [HttpPost]
        [Route("/create")]
        public IActionResult Create(FamilyModel input)
        {
            input.Title = "Create";
            if (input.Name == null || input.Name.Length < 3 || input.Name.Length > 128)
            {
                input.Message = "Invalid family name";
                return View(input);
            }

            if (User.Identity.IsAuthenticated)
            {
                User user = DbFunctions.FindUserByEmail(User.Identity.Name);
                if(user.FamilyId != null)
                {
                    input.Message = "You have already joined a family";
                    return View(input);
                }
                string name = input.Name;
                Family family = new Family();
                family.Name = name;
                DbFunctions.AddFamily(family);
                input.Code = family.Code;
                user.FamilyId = family.FamilyId;
                DbFunctions.UpdateUser(user);
                input.User.Name = user.Name;
                input.User.Surname = user.Surname;
                input.User.Email = user.Email;
            }
            else
            {
                return Redirect(Url.Action("login", "account") + "?ReturnUrl=/create");
            }
            return View(input);
        }


        [HttpGet]
        [Route("/join")]
        public IActionResult Join([FromQuery] string code = "")
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
                    return Redirect(Url.Action("login", "account") + "?ReturnUrl=/join");
                }
            }
            if(code != "")
            {
                return JoinPost(code);
            }
            return View(model);
        }

        [HttpPost]
        [Route("/join")]
        [ActionName("join")]
        public IActionResult JoinPost(string code)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect(Url.Action("login", "account") + "?ReturnUrl=/join");
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

        public IActionResult Leave()
        {
            if (User.Identity.IsAuthenticated)
            {
                User user = DbFunctions.FindUserByEmail(User.Identity.Name);
                user.FamilyId = null;
                DbFunctions.UpdateUser(user);
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
