namespace MyTrainer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yretku : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Chatrooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        messages = c.String(),
                        userId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.userId, cascadeDelete: true)
                .Index(t => t.userId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Chatrooms", "userId", "dbo.Users");
            DropIndex("dbo.Chatrooms", new[] { "userId" });
            DropTable("dbo.Chatrooms");
        }
    }
}
