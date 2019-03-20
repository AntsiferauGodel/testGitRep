namespace Lab08.MVC.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Files : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Files",
                c => new
                {
                    FileId = c.Int(nullable: false, identity: true),
                    FileName = c.String(),
                    ContentType = c.String(),
                    Content = c.Binary(),
                    FileType = c.Int(nullable: false),
                    AdvertisementId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.Advertisements", t => t.AdvertisementId, cascadeDelete: true)
                .Index(t => t.AdvertisementId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Files", "AdvertisementId", "dbo.Advertisements");
            DropIndex("dbo.Files", new[] { "AdvertisementId" });
            DropTable("dbo.Files");
        }
    }
}
