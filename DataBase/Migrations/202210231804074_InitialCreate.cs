namespace DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Metal",
                c => new
                    {
                        DateMet = c.String(nullable: false, maxLength: 128),
                        CodMet = c.String(nullable: false),
                        price = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.DateMet);
            
            CreateTable(
                "dbo.UserMetals",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        DateMet = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.DateMet })
                .ForeignKey("dbo.Metal", t => t.DateMet, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.DateMet);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        Login = c.String(nullable: false, maxLength: 20),
                        Password = c.String(nullable: false, maxLength: 40),
                        EmailFull = c.String(nullable: false),
                        EmailName = c.String(nullable: false),
                        EmailDomain = c.String(nullable: false),
                        EmailConfirmed = c.Boolean(nullable: false),
                        Name = c.String(nullable: false),
                        Surname = c.String(nullable: false),
                        Patronymic = c.String(),
                    })
                .PrimaryKey(t => t.UserId)
                .Index(t => t.Login, unique: true);
            
            CreateTable(
                "dbo.UserCurrency",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        Vcode = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.Vcode })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Сurrency", t => t.Vcode, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.Vcode);
            
            CreateTable(
                "dbo.Сurrency",
                c => new
                    {
                        Vcode = c.String(nullable: false, maxLength: 128),
                        Vname = c.String(nullable: false),
                        VEngname = c.String(nullable: false),
                        Vnom = c.String(nullable: false),
                        VcommonCode = c.String(nullable: false),
                        VnumCode = c.String(),
                        VcharCode = c.String(),
                    })
                .PrimaryKey(t => t.Vcode);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserMetals", "UserId", "dbo.User");
            DropForeignKey("dbo.UserCurrency", "Vcode", "dbo.Сurrency");
            DropForeignKey("dbo.UserCurrency", "UserId", "dbo.User");
            DropForeignKey("dbo.UserMetals", "DateMet", "dbo.Metal");
            DropIndex("dbo.UserCurrency", new[] { "Vcode" });
            DropIndex("dbo.UserCurrency", new[] { "UserId" });
            DropIndex("dbo.User", new[] { "Login" });
            DropIndex("dbo.UserMetals", new[] { "DateMet" });
            DropIndex("dbo.UserMetals", new[] { "UserId" });
            DropTable("dbo.Сurrency");
            DropTable("dbo.UserCurrency");
            DropTable("dbo.User");
            DropTable("dbo.UserMetals");
            DropTable("dbo.Metal");
        }
    }
}
