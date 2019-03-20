using Lab08.MVC.Domain;
using Microsoft.AspNet.Identity;

namespace Lab08.MVC.Data.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }
    }
}
