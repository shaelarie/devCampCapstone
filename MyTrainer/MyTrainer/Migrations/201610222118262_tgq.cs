namespace MyTrainer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tgq : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ChatComments", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "ChatComments");
        }
    }
}
