namespace PetesSalon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remodelServices : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductAndServices", "Description", c => c.String());
            AddColumn("dbo.ProductAndServices", "Type", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductAndServices", "Type");
            DropColumn("dbo.ProductAndServices", "Description");
        }
    }
}
