namespace MyTrainer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bgsws : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserSchedules", "data");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserSchedules", "data", c => c.String());
        }
    }
}
