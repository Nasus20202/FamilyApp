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
        public Family Family { get; set; }

        public ToDo ToDo { get; set; }
        public DateTime Time { get; set; }

        public List<ToDo> ToDos { get; set; }
    }
}
