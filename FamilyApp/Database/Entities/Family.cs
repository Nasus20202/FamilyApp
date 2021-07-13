using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyApp
{
    public class Family : EntityBase
    {
        public Family() : base()
        {
            using (var db = new Database()) {
                string code = "";
                do
                {
                    Random random = new Random();
                    for(int i = 0; i < 10; i++)
                    {
                        code += random.Next(10).ToString();
                    }
                } while (db.Families.Where(f => f.Code == code).Count() != 0);
                this.Code = code;
            }
        }

        [Key]
        public int FamilyId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public List<User> Users { get; set; }
    }
}
