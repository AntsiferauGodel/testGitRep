using System;
using System.Security.Claims;
using Lab08.MVC.Business.Models;
using Lab08.MVC.Domain;

namespace Lab08.MVC.Business.Interfaces
{
    public interface IUserService : IDisposable
    {
        OperationDetails Create(UserView userView);
        ClaimsIdentity Authenticate(UserView userView);
        ApplicationUser FindUserById(string userId);
    }
}
