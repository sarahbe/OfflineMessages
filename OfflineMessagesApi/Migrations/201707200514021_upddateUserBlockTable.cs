namespace OfflineMessagesApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upddateUserBlockTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserBlock",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        RecipientId = c.String(maxLength: 128),
                        Timestamp = c.DateTime(nullable: false),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.User", t => t.RecipientId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RecipientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserBlock", "UserId", "dbo.User");
            DropForeignKey("dbo.UserBlock", "RecipientId", "dbo.User");
            DropIndex("dbo.UserBlock", new[] { "RecipientId" });
            DropIndex("dbo.UserBlock", new[] { "UserId" });
            DropTable("dbo.UserBlock");
        }
    }
}
