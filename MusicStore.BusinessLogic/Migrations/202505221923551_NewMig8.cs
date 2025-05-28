namespace MusicStore.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMig8 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SubscriptionDatas", "Id", "dbo.AppUsers");
            DropIndex("dbo.SubscriptionDatas", new[] { "Id" });
            DropTable("dbo.PlanDatas");
            DropTable("dbo.SubscriptionDatas");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
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
            
            CreateIndex("dbo.SubscriptionDatas", "Id");
            AddForeignKey("dbo.SubscriptionDatas", "Id", "dbo.AppUsers", "Id");
        }
    }
}
