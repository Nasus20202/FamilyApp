using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyApp
{
    public class ToDo : EntityBase
    {
        public ToDo() : base() { this.Done = false; }

        [Key]
        public int ToDoId { get; set; }

        public string Name { get; set; }

        public string Action { get; set; }
        public bool Done { get; set; }
        public DateTime? Deadline { get; set; }
        public byte Importance { get; set; }

        public User User { get; set; }

        public int? UserId { get; set; }

        public Family Family { get; set; }
        public int FamilyId { get; set; }
    }
}
