using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Lab08.MVC.Business.Interfaces;
using Lab08.MVC.Business.Services;
using Lab08.MVC.Data.DbContext;
using Lab08.MVC.Data.Identity;
using Lab08.MVC.Data.Interfaces;
using Lab08.MVC.Data.Repositories;
using Lab08.MVC.Data.Uows;
using Lab08.MVC.Domain;
using Lab08.MVC.Web.Logger;

namespace Lab08.MVC.Web.AutofacConfigs
{
    public static class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<Log>().AsSelf();
            builder.RegisterType<FleaMarketContext>().AsSelf();
            builder.RegisterType<FleaMarketContext>().As<DbContext>();
            builder.RegisterType<FileManager>().As<IFileManager>();
            builder.RegisterType<FileService>().As<IFileService>();
            builder.RegisterType<RoleStore<ApplicationRole>>().As<IRoleStore<ApplicationRole, string>>();
            builder.RegisterType<UserStore<ApplicationUser>>().As<IUserStore<ApplicationUser>>();
            builder.RegisterType<ApplicationUserManager>().AsSelf();
            builder.RegisterType<ApplicationRoleManager>().AsSelf();
            builder.RegisterType<AdvertisementManager>().As<IAdvertisementManager>();
            builder.RegisterType<FleaMarketUnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<AdvertisementService>().As<IAdvertisementService>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<PrincipalService>().As<IPrincipalService>();
            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}