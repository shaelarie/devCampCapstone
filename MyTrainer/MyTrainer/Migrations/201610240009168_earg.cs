namespace MyTrainer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class earg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Meal1", "calories", c => c.Double());
            AddColumn("dbo.Meal2", "calories", c => c.Double());
            AddColumn("dbo.Meal3", "calories", c => c.Double());
            AddColumn("dbo.Snack1", "calories", c => c.Double());
            AddColumn("dbo.Snack2", "calories", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Snack2", "calories");
            DropColumn("dbo.Snack1", "calories");
            DropColumn("dbo.Meal3", "calories");
            DropColumn("dbo.Meal2", "calories");
            DropColumn("dbo.Meal1", "calories");
        }
    }
}
