using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyApp
{
    public class DbFunctions
    { 
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
