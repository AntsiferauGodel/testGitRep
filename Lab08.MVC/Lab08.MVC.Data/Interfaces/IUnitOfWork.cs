using System;
using Lab08.MVC.Data.Identity;

namespace Lab08.MVC.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RoleManager { get; }
        IAdvertisementManager AdvertisementManager { get; }
        IFileManager FileManager { get; }
        void Save();
    }
}
