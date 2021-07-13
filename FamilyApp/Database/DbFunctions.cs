using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyApp
{
    public class DbFunctions
    {
        // ToDos

        public static ToDo FindToDoById(int id)
        {
            using (var db = new Database())
            {
                var todo = (from f in db.ToDos
                              where f.ToDoId == id
                              select f).FirstOrDefault();
                return todo;
            }
        }

        public static void AddTodo(ToDo todo)
        {
            if (todo == null)
                return;
            using (var db = new Database())
            {
                db.ToDos.Add(todo);
                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    return;
                }
            }
        }
        public static void UpdateToDo(ToDo updatedTodo)
        {
            using (var db = new Database())
            {
                var todo = (from f in db.ToDos
                              where f.ToDoId == updatedTodo.ToDoId
                              select f).FirstOrDefault();
                if (todo == null)
                    return;

                todo.Name = updatedTodo.Name;
                todo.Action = updatedTodo.Action;
                todo.Done = updatedTodo.Done;
                todo.Deadline = updatedTodo.Deadline;
                todo.Importance = updatedTodo.Importance;
                todo.UserId = updatedTodo.UserId;
                todo.FamilyId = updatedTodo.FamilyId;

                todo.Modified = DateTime.UtcNow;
                try
                {
                    db.SaveChanges();
                }
                catch
                {

                }
            }
        }


        // Families

        public static Family FindFamilyById(int id)
        {
            using (var db = new Database())
            {
                var family = (from f in db.Families
                            where f.FamilyId == id
                            select f).FirstOrDefault();
                return family;
            }
        }

        public static void AddFamily(Family family)
        {
            if (family == null)
                return;
            using (var db = new Database())
            {
                db.Families.Add(family);
                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    return;
                }
            }
        }
        public static void UpdateFamily(Family updatedFamily)
        {
            using (var db = new Database())
            {
                var family = (from f in db.Families
                              where f.FamilyId == updatedFamily.FamilyId
                              select f).FirstOrDefault();
                if (family == null)
                    return;
                family.Name = updatedFamily.Name;
                family.Code = updatedFamily.Code;
                family.Enabled = family.Enabled;

                family.Modified = DateTime.UtcNow;
                try
                {
                    db.SaveChanges();
                }
                catch
                {

                }
            }
        }

        //Users

        public static User FindUserById(int id)
        {
            using (var db = new Database())
            {
                var user = (from c in db.Users
                                where c.UserId == id
                                select c).FirstOrDefault();
                return user;
            }
        }
        public static User FindUserByEmail(string email)
        {
            using (var db = new Database())
            {
                var user = (from c in db.Users
                            where c.Email == email
                            select c).FirstOrDefault();
                return user;
            }
        }
        public static void AddUser(User user)
        {
            if (user == null)
                return;
            using (var db = new Database())
            {
                db.Users.Add(user);
                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    return;
                }
            }
        }
        public static void UpdateUser(User updatedUser)
        {
            using (var db = new Database())
            {
                var user = (from c in db.Users
                            where c.UserId == updatedUser.UserId
                            select c).FirstOrDefault();
                if (user == null)
                    return;
                user.Name = updatedUser.Name;
                user.Surname = updatedUser.Surname;
                user.Email = updatedUser.Email;
                user.Role = updatedUser.Role;
                user.Password = updatedUser.Password;
                user.FamilyId = updatedUser.FamilyId;
                user.Enabled = updatedUser.Enabled;

                user.Modified = DateTime.UtcNow;
                try
                {
                    db.SaveChanges();
                }
                catch
                {

                }
            }
        }
    }
}
