namespace FormASPNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUrlImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "UrlImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "UrlImage");
        }
    }
}
