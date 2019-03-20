using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Lab08.MVC.Business.Interfaces;
using Lab08.MVC.Business.Models;
using Lab08.MVC.Data.Interfaces;
using Lab08.MVC.Domain;

namespace Lab08.MVC.Business.Services
{
    public class UserService : IUserService
    {
        private const string UserExistsMessage = "User with this login already exists";
        private const string UserRegisteredMessage = "user registered";
        private const string EmailPropertyName = "Email";
        public IUnitOfWork UnitOfWork { get; set; }

        public UserService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public ApplicationUser FindUserById(string userId)
        {
            return UnitOfWork.UserManager.FindById(userId);
        }
        public OperationDetails Create(UserView userView)
        {
            var user = UnitOfWork.UserManager.FindByEmail(userView.Email);
            if (user != null)
            {
                return new OperationDetails(false, UserExistsMessage, EmailPropertyName);
            }
            user = new ApplicationUser { Email = userView.Email, UserName = userView.UserName };
            var result = UnitOfWork.UserManager.Create(user, userView.Password);
            if (result.Errors.Any())
            {
                return new OperationDetails(false, result.Errors.FirstOrDefault());
            }
            UnitOfWork.UserManager.AddToRole(user.Id, userView.Role);
            return new OperationDetails(true, UserRegisteredMessage);
        }

        public ClaimsIdentity Authenticate(UserView userView)
        {
            ClaimsIdentity claim = null;
            var emailUser = UnitOfWork.UserManager.FindByEmail(userView.Email);
            if (emailUser != null)
            {
                var checkResult = UnitOfWork.UserManager.CheckPassword(emailUser, userView.Password);
                if (checkResult)
                {
                    claim = UnitOfWork.UserManager.CreateIdentity(
                        emailUser,
                        DefaultAuthenticationTypes.ApplicationCookie
                    );
                    return claim;
                }
            }
            var user = UnitOfWork.UserManager.Find(userView.UserName, userView.Password);

            if (user != null)
            {
                claim = UnitOfWork.UserManager.CreateIdentity(
                    user,
                    DefaultAuthenticationTypes.ApplicationCookie
                    );
            }
            return claim;
        }

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}
