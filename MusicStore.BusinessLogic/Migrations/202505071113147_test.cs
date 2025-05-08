namespace MusicStore.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppUsers", "Token", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppUsers", "Token");
        }
    }
}
