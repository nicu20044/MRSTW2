namespace MusicStore.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMig1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlanDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DurationDays = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SubscriptionDatas",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        PlanName = c.String(nullable: false, maxLength: 50),
                        StartDate = c.DateTime(nullable: false),
                        ExpiryDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppUsers", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubscriptionDatas", "Id", "dbo.AppUsers");
            DropIndex("dbo.SubscriptionDatas", new[] { "Id" });
            DropTable("dbo.SubscriptionDatas");
            DropTable("dbo.PlanDatas");
        }
    }
}
