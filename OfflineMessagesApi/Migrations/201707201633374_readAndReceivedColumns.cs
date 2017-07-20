namespace OfflineMessagesApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class readAndReceivedColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Message", "ReceivedDate", c => c.DateTime());
            AddColumn("dbo.Message", "ReadDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Message", "ReadDate");
            DropColumn("dbo.Message", "ReceivedDate");
        }
    }
}
