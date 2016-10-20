namespace MyTrainer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fefe : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "BMR", c => c.Double());
            AddColumn("dbo.Users", "WorkoutAmount", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "WorkoutAmount");
            DropColumn("dbo.Users", "BMR");
        }
    }
}
