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
                model.ToDo = new ToDo();
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

        [HttpPost]
        public IActionResult AddNewTask(FamilyModel input)
        {
            User user = DbFunctions.FindUserByEmail(User.Identity.Name);
            if (user.FamilyId == null)
                return Forbid();
            ToDo toDo = input.ToDo;
            if(toDo.Name == null)
                return Redirect("/family");
            if (toDo.Deadline != null)
            {
                DateTime dateTime = (DateTime) toDo.Deadline;
                dateTime = dateTime.AddHours(input.Time.Hour);
                dateTime = dateTime.AddMinutes(input.Time.Minute);
                toDo.Deadline = dateTime;
            }
            else if(input.Time.Year != 1)
            {
                toDo.Deadline = input.Time;
            }
            if (toDo.Name != null && toDo.Name.Length > 128)
                toDo.Name.Substring(0, 128);
            if (toDo.Action != null && toDo.Action.Length > 2048)
                toDo.Action.Substring(0, 2048);
            toDo.FamilyId = (int) user.FamilyId;
            if(toDo.UserId != null)
            {
                User userWithTask = DbFunctions.FindUserById((int) toDo.UserId);
                if (userWithTask == null || userWithTask.FamilyId != user.FamilyId)
                    toDo.UserId = null;
            }
            if (toDo.Importance < 1)
                toDo.Importance = 1;
            else if (toDo.Importance > 5)
                toDo.Importance = 5;
            DbFunctions.AddTodo(toDo);
            return Redirect("/family");
        }
    }
}
