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
        public IActionResult Index(int oldMessages = 30)
        {
            if (oldMessages < 0)
                oldMessages = 0;
            if (oldMessages > 1000)
                oldMessages = 1000;
            FamilyModel model = new FamilyModel("Home");
            using (var db = new Database())
            {
                User user = DbFunctions.FindUserByEmail(User.Identity.Name);
                model.User.Name = user.Name;
                model.User.Surname = user.Surname;
                model.User.Email = user.Email;
                model.User.FamilyId = user.FamilyId;
                if (user.FamilyId == null)
                    return Redirect("/");
                model.Family = DbFunctions.FindFamilyById((int)user.FamilyId);
                model.ToDos = db.ToDos.Where(td => td.FamilyId == model.User.FamilyId && !td.Done).OrderByDescending(td => td.Importance).ThenBy(td => td.Deadline).ToList();
                model.ShoppingList = db.Products.Where(p => p.FamilyId == model.User.FamilyId && p.Enabled).OrderBy(p => p.Name).ThenByDescending(p => p.Amount).ToList();
                model.Messages = db.Messages.Where(m => m.FamilyId == model.User.FamilyId).OrderByDescending(m => m.Time).Take(oldMessages).OrderBy(m => m.Time).ToList();
                model.ToDo = new ToDo();
                model.Product = new Product();
            }
            ViewData["oldMessages"] = oldMessages;
            return View(model);
        }

        [HttpPost]
        public IActionResult MarkToDoAsDone([FromQuery] int id)
        {
            using (var db = new Database())
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
            if (toDo.Name == null)
                return Redirect("/family");
            if (toDo.Deadline != null)
            {
                DateTime dateTime = (DateTime)toDo.Deadline;
                dateTime = dateTime.AddHours(input.Time.Hour);
                dateTime = dateTime.AddMinutes(input.Time.Minute);
                toDo.Deadline = dateTime;
            }
            else if (input.Time.Year != 1)
            {
                toDo.Deadline = input.Time;
            }
            if (toDo.Name != null && toDo.Name.Length > 128)
                toDo.Name.Substring(0, 128);
            if (toDo.Action != null && toDo.Action.Length > 2048)
                toDo.Action.Substring(0, 2048);
            toDo.FamilyId = (int)user.FamilyId;
            if (toDo.UserId != null)
            {
                User userWithTask = DbFunctions.FindUserById((int)toDo.UserId);
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

        [HttpPost]
        public IActionResult RemoveProduct([FromQuery] int id)
        {
            using (var db = new Database())
            {
                Product product = db.Products.Where(td => td.ProductId == id).FirstOrDefault();
                if (product == null)
                    return NotFound();
                User user = DbFunctions.FindUserByEmail(User.Identity.Name);
                if (product.FamilyId != user.FamilyId)
                    return Forbid();
                product.Enabled = false;
                DbFunctions.UpdateProduct(product);
            }
            return Ok();
        }

        [HttpPost]
        public IActionResult AddNewProduct(FamilyModel input)
        {
            User user = DbFunctions.FindUserByEmail(User.Identity.Name);
            if (user.FamilyId == null)
                return Forbid();
            Product product = input.Product;
            product.FamilyId = (int) user.FamilyId;
            if (product.Name == null || product.Name.Length < 1)
                return BadRequest();
            if (product.Name.Length > 128)
                product.Name = product.Name.Substring(0, 128);
            if (product.Amount != null && product.Amount.Length > 32)
                product.Amount = product.Amount.Substring(0, 32);
            DbFunctions.AddProduct(product);
            return Redirect("/family");
        }

    } 
}
