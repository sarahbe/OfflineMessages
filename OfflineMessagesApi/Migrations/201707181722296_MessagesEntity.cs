namespace OfflineMessagesApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MessagesEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Message",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SenderId = c.String(maxLength: 128),
                        RecipientId = c.String(maxLength: 128),
                        Body = c.String(),
                        Timestamp = c.DateTime(nullable: false),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.User", t => t.RecipientId)
                .ForeignKey("dbo.User", t => t.SenderId)
                .Index(t => t.SenderId)
                .Index(t => t.RecipientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Message", "SenderId", "dbo.User");
            DropForeignKey("dbo.Message", "RecipientId", "dbo.User");
            DropIndex("dbo.Message", new[] { "RecipientId" });
            DropIndex("dbo.Message", new[] { "SenderId" });
            DropTable("dbo.Message");
        }
    }
}
