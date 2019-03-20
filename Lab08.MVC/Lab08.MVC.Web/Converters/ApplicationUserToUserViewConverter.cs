using Lab08.MVC.Business.Models;
using Lab08.MVC.Domain;

namespace Lab08.MVC.Web.Converters
{
    public static class ApplicationUserToUserViewConverter
    {
        public static UserView Convert(ApplicationUser user)
        {
            return new UserView
            {
                Email = user.Email,
                Id = user.Id,
                Name = user.UserName
            };
        }
    }
}
