using System;
using Lab08.MVC.Data.DbContext;
using Lab08.MVC.Data.Identity;
using Lab08.MVC.Data.Interfaces;

namespace Lab08.MVC.Data.Uows
{
    public class FleaMarketUnitOfWork : IUnitOfWork
    {
        private bool disposed = false;
        private readonly FleaMarketContext context;
        public ApplicationUserManager UserManager { get; private set; }
        public ApplicationRoleManager RoleManager { get; private set; }
        public IAdvertisementManager AdvertisementManager { get; private set; }
        public IFileManager FileManager { get; private set; }

        public FleaMarketUnitOfWork(
            IFileManager fileManager,
            IAdvertisementManager advertisementManager,
            ApplicationRoleManager applicationRoleManager,
            FleaMarketContext fleaMarketContext,
            ApplicationUserManager applicationUserManager
            )
        {
            context = fleaMarketContext;
            UserManager = applicationUserManager;
            RoleManager = applicationRoleManager;
            AdvertisementManager = advertisementManager;
            FileManager = fileManager;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    UserManager.Dispose();
                    RoleManager.Dispose();
                    AdvertisementManager.Dispose();
                }
            }
        }
    }
}
