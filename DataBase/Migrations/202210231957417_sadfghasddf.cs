namespace DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sadfghasddf : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserMetals", "DateMet", "dbo.Metal");
            RenameColumn(table: "dbo.UserMetals", name: "DateMet", newName: "CodMet");
            RenameIndex(table: "dbo.UserMetals", name: "IX_DateMet", newName: "IX_CodMet");
            DropPrimaryKey("dbo.Metal");
            AddColumn("dbo.Metal", "NameMet", c => c.String(nullable: false));
            AlterColumn("dbo.Metal", "CodMet", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Metal", "CodMet");
            AddForeignKey("dbo.UserMetals", "CodMet", "dbo.Metal", "CodMet", cascadeDelete: true);
            DropColumn("dbo.Metal", "DateMet");
            DropColumn("dbo.Metal", "price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Metal", "price", c => c.String(nullable: false));
            AddColumn("dbo.Metal", "DateMet", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.UserMetals", "CodMet", "dbo.Metal");
            DropPrimaryKey("dbo.Metal");
            AlterColumn("dbo.Metal", "CodMet", c => c.String(nullable: false));
            DropColumn("dbo.Metal", "NameMet");
            AddPrimaryKey("dbo.Metal", "DateMet");
            RenameIndex(table: "dbo.UserMetals", name: "IX_CodMet", newName: "IX_DateMet");
            RenameColumn(table: "dbo.UserMetals", name: "CodMet", newName: "DateMet");
            AddForeignKey("dbo.UserMetals", "DateMet", "dbo.Metal", "DateMet", cascadeDelete: true);
        }
    }
}
