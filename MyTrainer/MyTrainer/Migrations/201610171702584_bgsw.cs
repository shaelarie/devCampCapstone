namespace MyTrainer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bgsw : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserSchedules", "data", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserSchedules", "data");
        }
    }
}
