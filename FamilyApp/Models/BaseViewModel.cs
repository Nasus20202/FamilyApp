using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyApp
{
    public class BaseViewModel
    {
        public BaseViewModel(string title = "", string message = "")
        {
            Title = title;
            Message = message;
            User = new UserData();
        }
        public string Title { get; set; }
        public string Message { get; set; }
        public UserData User { get; set; }
    }
    public class UserData
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public int? FamilyId { get; set; }
    }
}
