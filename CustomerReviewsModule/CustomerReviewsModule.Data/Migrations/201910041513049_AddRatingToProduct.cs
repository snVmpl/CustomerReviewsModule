namespace CustomerReviewsModule.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddRatingToProduct : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RatingProduct",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Rating = c.Double(),
                })
                .PrimaryKey(t => t.Id);
            AddForeignKey("dbo.RatingProduct", "Id", "dbo.Item", "Id");

            Sql("INSERT INTO dbo.RatingProduct (Id) SELECT Id FROM dbo.Item");
        }

        public override void Down()
        {
            DropTable("dbo.RatingProduct");
        }
    }
}
