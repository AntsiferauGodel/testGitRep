using Lab08.MVC.Domain;
using Microsoft.AspNet.Identity;

namespace Lab08.MVC.Data.Identity
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole, string> store)
            : base(store)
        {
        }
    }
}
