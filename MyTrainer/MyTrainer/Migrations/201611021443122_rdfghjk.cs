namespace MyTrainer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rdfghjk : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Chatrooms", "name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Chatrooms", "name");
        }
    }
}
