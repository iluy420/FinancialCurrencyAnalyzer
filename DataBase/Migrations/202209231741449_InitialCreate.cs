namespace DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        Login = c.String(nullable: false, maxLength: 20),
                        Password = c.String(nullable: false, maxLength: 20),
                        EmailFull = c.String(nullable: false),
                        EmailName = c.String(nullable: false),
                        EmailDomain = c.String(nullable: false),
                        EmailConfirmed = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Surname = c.String(nullable: false),
                        Patronymic = c.String(),
                    })
                .PrimaryKey(t => t.UserId)
                .Index(t => t.Login, unique: true);
            
            CreateTable(
                "dbo.Сurrency",
                c => new
                    {
                        СurrencyId = c.Guid(nullable: false),
                        LetterCode = c.String(maxLength: 3),
                        DigitalCode = c.String(maxLength: 3),
                        Name = c.String(nullable: false),
                        Сourse = c.Single(nullable: false),
                        Forecast = c.Single(nullable: false),
                        DateOfUpdate = c.DateTime(nullable: false),
                        DateDeleteInfo = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.СurrencyId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.User", new[] { "Login" });
            DropTable("dbo.Сurrency");
            DropTable("dbo.User");
        }
    }
}
