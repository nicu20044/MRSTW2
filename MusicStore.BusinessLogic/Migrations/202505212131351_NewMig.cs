namespace MusicStore.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserCartItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductDatas", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.AppUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserCartItems", "UserId", "dbo.AppUsers");
            DropForeignKey("dbo.UserCartItems", "ProductId", "dbo.ProductDatas");
            DropIndex("dbo.UserCartItems", new[] { "ProductId" });
            DropIndex("dbo.UserCartItems", new[] { "UserId" });
            DropTable("dbo.UserCartItems");
        }
    }
}
