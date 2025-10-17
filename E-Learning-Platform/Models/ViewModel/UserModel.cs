using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Learning_Platform.Models.ViewModel
{
    public class UserModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public long Phone { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Role { get; set; }
    }
}