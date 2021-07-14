using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyApp
{
    public class Product : EntityBase
    {
        [Key]
        public int ProductId { get; set; }


        public string Name { get; set; }
        public string Amount { get; set; }

        public int FamilyId { get; set; }
        public Family Family { get; set; }
    }
}
