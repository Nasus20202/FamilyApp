using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyApp
{
    public class FamilyModel : BaseViewModel
    {
        public FamilyModel(string title) : base(title) { }
        public FamilyModel() : base("") { }

        public string Name { get; set; }
        public string Code { get; set; }
        public List<int> Users { get; set; }
    }
}
