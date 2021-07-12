using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyApp
{
    public class AccountModel : BaseViewModel
    {
        public AccountModel(string title = "", string message = "") : base(title, message) {}
        public AccountModel(string title = "") : base(title, "") { }
        public AccountModel() : base("", "") { }

        public string Login { get; set; }
        public string Password { get; set; }
        public string Password2 { get; set; }
        public string OldPassword { get; set; }
        public bool RememberMe { get; set; }
    }
}
