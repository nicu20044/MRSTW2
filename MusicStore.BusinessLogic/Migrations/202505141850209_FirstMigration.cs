namespace MusicStore.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                        Genre = c.String(maxLength: 50),
                        ProducerId = c.Int(nullable: false),
                        Bpm = c.Int(nullable: false),
                        ImageUrl = c.String(maxLength: 255),
                        ReleaseDate = c.String(),
                        ArtistName = c.String(),
                        Scale = c.String(),
                        AudioFileUrl = c.String(),
                        UploadDate = c.DateTime(nullable: false),
                        License = c.String(),
                        ArtistId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AppUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Token = c.String(),
                        Email = c.String(nullable: false),
                        PasswordHash = c.String(nullable: false, maxLength: 50),
                        UserRole = c.String(),
                        LastLoginTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AppUsers");
            DropTable("dbo.ProductDatas");
        }
    }
}
