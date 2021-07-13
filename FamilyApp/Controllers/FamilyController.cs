using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyApp
{
    [Authorize]
    public class FamilyController : Controller
    {
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
                model.ToDos = db.ToDos.Where(td => !td.Done && td.FamilyId == model.User.FamilyId).OrderByDescending(td => td.Importance).ThenBy(td => td.Deadline).ToList();
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult MarkToDoAsDone([FromQuery] int id)
        {
            using(var db = new Database())
            {
                ToDo todo = db.ToDos.Where(td => td.ToDoId == id).FirstOrDefault();
                if (todo == null)
                    return NotFound();
                User user = DbFunctions.FindUserByEmail(User.Identity.Name);
                if (todo.FamilyId != user.FamilyId)
                    return Forbid();
                todo.Done = true;
                DbFunctions.UpdateToDo(todo);
            }
            return Ok();
        }
    }
}
