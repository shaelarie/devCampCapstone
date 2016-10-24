namespace MyTrainer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class revaevr : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "ChatComments");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "ChatComments", c => c.String());
        }
    }
}
