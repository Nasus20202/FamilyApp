using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyApp
{
    public class FamilyController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            FamilyModel model = new FamilyModel("Home");
            using(var db = new Database())
            {
                User user = DbFunctions.FindUserByEmail(User.Identity.Name);
                model.User.Name = user.Name;
                model.User.Surname = user.Surname;
                model.User.Email = user.Email;
                model.User.FamilyId = user.FamilyId;
                if (user.FamilyId == null)
                    return Redirect("/");
                model.Family = DbFunctions.FindFamilyById((int) user.FamilyId);
                model.ToDos = db.ToDos.Where(td => !td.Done).OrderBy(td => td.Deadline).ToList();
            }
            return View(model);
        }
    }
}
