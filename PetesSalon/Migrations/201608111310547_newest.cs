namespace PetesSalon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newest : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ProductAndServices", "ProductType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductAndServices", "ProductType", c => c.String());
        }
    }
}
