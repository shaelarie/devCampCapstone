namespace MyTrainer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eargrfg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Meal1", "protein", c => c.Double());
            AddColumn("dbo.Meal1", "fat", c => c.Double());
            AddColumn("dbo.Meal1", "carbs", c => c.Double());
            AddColumn("dbo.Meal2", "protein", c => c.Double());
            AddColumn("dbo.Meal2", "fat", c => c.Double());
            AddColumn("dbo.Meal2", "carbs", c => c.Double());
            AddColumn("dbo.Meal3", "protein", c => c.Double());
            AddColumn("dbo.Meal3", "fat", c => c.Double());
            AddColumn("dbo.Meal3", "carbs", c => c.Double());
            AddColumn("dbo.Snack1", "protein", c => c.Double());
            AddColumn("dbo.Snack1", "fat", c => c.Double());
            AddColumn("dbo.Snack1", "carbs", c => c.Double());
            AddColumn("dbo.Snack2", "protein", c => c.Double());
            AddColumn("dbo.Snack2", "fat", c => c.Double());
            AddColumn("dbo.Snack2", "carbs", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Snack2", "carbs");
            DropColumn("dbo.Snack2", "fat");
            DropColumn("dbo.Snack2", "protein");
            DropColumn("dbo.Snack1", "carbs");
            DropColumn("dbo.Snack1", "fat");
            DropColumn("dbo.Snack1", "protein");
            DropColumn("dbo.Meal3", "carbs");
            DropColumn("dbo.Meal3", "fat");
            DropColumn("dbo.Meal3", "protein");
            DropColumn("dbo.Meal2", "carbs");
            DropColumn("dbo.Meal2", "fat");
            DropColumn("dbo.Meal2", "protein");
            DropColumn("dbo.Meal1", "carbs");
            DropColumn("dbo.Meal1", "fat");
            DropColumn("dbo.Meal1", "protein");
        }
    }
}
