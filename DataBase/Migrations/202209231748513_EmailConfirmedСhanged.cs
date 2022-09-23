namespace DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmailConfirmedСhanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "EmailConfirmed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "EmailConfirmed", c => c.Int(nullable: false));
        }
    }
}
