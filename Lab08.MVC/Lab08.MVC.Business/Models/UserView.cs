using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Lab08.MVC.Business.Models
{
    public class UserView
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}
