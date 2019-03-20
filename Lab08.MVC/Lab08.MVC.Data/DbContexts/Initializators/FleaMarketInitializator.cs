using System.Collections.Generic;
using System.Data.Entity;
using Lab08.MVC.Data.DbContext;
using Lab08.MVC.Data.Identity;
using Lab08.MVC.Data.RoleEnums;
using Lab08.MVC.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Lab08.MVC.Data.DbContexts.Initializators
{
    public class FleaMarketInitializator : CreateDatabaseIfNotExists<FleaMarketContext>
    {
        protected override void Seed(FleaMarketContext context)
        {
            const string name = "ExampleName";
            const string pass = "testPassword";
            const string mailDomain = "@mail.ru";
            const string advertisementHeader = "Some advertisement header";
            const string advertisementDescription = "Some advertisement description";
            const decimal advertisementPrice = 345.3m;
            const int amountOfAdvertisements = 5;
            const int amountOfFirstRoleUsers = 5;
            const int amountOfSecondRoleUsers = 5;
            var applicationUserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var applicationRoleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));
            var applicationUsers = new List<ApplicationUser>();
            var advertisements = new List<Advertisement>();
            for (var i = 0; i < amountOfSecondRoleUsers; i++)
            {
                applicationUsers.Add(new ApplicationUser
                {
                    Email = Roles.Trader.ToString() + i + mailDomain,
                    UserName = Roles.Trader.ToString() + i + name
                });
            }
            for (var i = 0; i < amountOfAdvertisements; i++)
            {
                advertisements.Add(new Advertisement
                {
                    Header = advertisementHeader + i,
                    Description = advertisementDescription + i,
                    Price = advertisementPrice + i,
                    CreatorUser = applicationUsers[i]
                });
            }

            for (var i = 0; i < amountOfFirstRoleUsers; i++)
            {
                applicationUsers.Add(new ApplicationUser
                {
                    Email = Roles.Buyer.ToString() + i + mailDomain,
                    UserName = Roles.Buyer.ToString() + i + name
                });
            }
            var roles = new List<string>
            {
                Roles.Trader.ToString(),
                Roles.Buyer.ToString()
            };
            foreach (var roleName in roles)
            {
                var role = applicationRoleManager.FindByName(roleName); // creating roles if not exists
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    applicationRoleManager.Create(role);
                }
            }
            foreach (var user in applicationUsers)
            {
                var userDb = applicationUserManager.FindByEmail(user.Email);
                if (userDb == null)
                {
                    applicationUserManager.Create(user, pass);
                    applicationUserManager.AddToRole(user.Id, user.UserName.Contains(Roles.Buyer.ToString()) ? Roles.Buyer.ToString() : Roles.Trader.ToString());
                }
            }
            context.Advertisements.AddRange(advertisements);
            base.Seed(context);
        }
    }
}
