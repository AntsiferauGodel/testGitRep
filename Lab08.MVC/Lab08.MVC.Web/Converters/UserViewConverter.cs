using Lab08.MVC.Business.Models;

namespace Lab08.MVC.Web.Converters
{
    public static class UserViewConverter
    {
        public static UserView ConvertRegisterToUserView(UserRegisterModel model)
        {
            return new UserView
            {
                Email = model.Email,
                Name = model.Name,
                Password = model.Password,
                UserName = model.Name,
                Role = model.RoleEnum.ToString()
            };
        }

        public static UserView ConvertLoginToUserView(LoginModel model)
        {
            return new UserView
            {
                Email = model.Login,
                UserName = model.Login,
                Password = model.Password
            };
        }
    }
}
