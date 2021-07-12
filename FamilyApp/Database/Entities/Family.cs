using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyApp
{
    public class Family : EntityBase
    {
        [Key]
        public int FamilyId { get; set; }

        public List<User> Users { get; set; }
    }
}
