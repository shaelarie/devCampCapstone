namespace MyTrainer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cleanTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserSchedules", "startTime", c => c.String());
            AddColumn("dbo.UserSchedules", "endTime", c => c.String());
            AddColumn("dbo.UserSchedules", "background", c => c.String());
            AddColumn("dbo.UserSchedules", "editable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserSchedules", "editable");
            DropColumn("dbo.UserSchedules", "background");
            DropColumn("dbo.UserSchedules", "endTime");
            DropColumn("dbo.UserSchedules", "startTime");
        }
    }
}
