using System.Data.Entity;
using Lab08.MVC.Data.DbContexts.Initializators;
using Lab08.MVC.Data.EntityConfigurations;
using Lab08.MVC.Domain;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Lab08.MVC.Data.DbContext
{
    public class FleaMarketContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<File> Files { get; set; }

        public FleaMarketContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer( new FleaMarketInitializator());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AdvertisementEntityConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
