namespace CustomerReviewsModule.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ExtensionCustomerReview : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerReview", "Pros", c => c.String(maxLength: 256));
            AddColumn("dbo.CustomerReview", "Cons", c => c.String(maxLength: 256));
            AddColumn("dbo.CustomerReview", "Rating", c => c.Byte(nullable: false, defaultValue: 3));
        }

        public override void Down()
        {
            DropColumn("dbo.CustomerReview", "Rating");
            DropColumn("dbo.CustomerReview", "Cons");
            DropColumn("dbo.CustomerReview", "Pros");
        }
    }
}
