using System.Security.Principal;

namespace Lab08.MVC.Business.Interfaces
{
    public interface IPrincipalService
    {
        string GetUserIdFromPrincipal(IPrincipal principal);
    }
}
