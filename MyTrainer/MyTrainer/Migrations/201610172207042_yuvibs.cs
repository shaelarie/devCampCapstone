namespace MyTrainer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yuvibs : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserSchedules", "start", c => c.DateTime());
            AlterColumn("dbo.UserSchedules", "end", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserSchedules", "end", c => c.DateTime(nullable: false));
            AlterColumn("dbo.UserSchedules", "start", c => c.DateTime(nullable: false));
        }
    }
}
