namespace MusicStore.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Newmigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserCartItems", "ProductId", "dbo.ProductDatas");
            DropForeignKey("dbo.UserCartItems", "UserId", "dbo.AppUsers");
            DropIndex("dbo.UserCartItems", new[] { "UserId" });
            DropIndex("dbo.UserCartItems", new[] { "ProductId" });
            DropTable("dbo.UserCartItems");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserCartItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.UserCartItems", "ProductId");
            CreateIndex("dbo.UserCartItems", "UserId");
            AddForeignKey("dbo.UserCartItems", "UserId", "dbo.AppUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserCartItems", "ProductId", "dbo.ProductDatas", "Id", cascadeDelete: true);
        }
    }
}
