namespace PetesSalon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addProductType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductAndServices", "ProductType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductAndServices", "ProductType");
        }
    }
}
