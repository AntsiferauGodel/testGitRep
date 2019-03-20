namespace Lab08.MVC.Data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Lab08.MVC.Data.DbContext.FleaMarketContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Lab08.MVC.Data.DbContext.FleaMarketContext";
        }
    }
}
