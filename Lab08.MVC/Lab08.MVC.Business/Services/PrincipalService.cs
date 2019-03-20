using System.Security.Principal;
using Lab08.MVC.Business.Interfaces;
using Microsoft.AspNet.Identity;

namespace Lab08.MVC.Business.Services
{
    public class PrincipalService : IPrincipalService
    {
        public string GetUserIdFromPrincipal(IPrincipal principal)
        {
            return principal.Identity.GetUserId();
        }
    }
}
