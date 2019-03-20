using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Lab08.MVC.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Advertisement> Advertisements { get; set; }
    }
}
