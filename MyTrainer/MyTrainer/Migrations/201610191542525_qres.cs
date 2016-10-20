namespace MyTrainer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class qres : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "TDEE", c => c.Double());
            AddColumn("dbo.Users", "DailyCalorieIntake", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "DailyCalorieIntake");
            DropColumn("dbo.Users", "TDEE");
        }
    }
}
