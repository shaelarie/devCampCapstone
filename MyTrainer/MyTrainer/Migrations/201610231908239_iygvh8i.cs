namespace MyTrainer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class iygvh8i : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Meal1", "servingSize", c => c.Double(nullable: false));
            AddColumn("dbo.Meal2", "servingSize", c => c.Double(nullable: false));
            AddColumn("dbo.Meal3", "servingSize", c => c.Double(nullable: false));
            AddColumn("dbo.Snack1", "servingSize", c => c.Double(nullable: false));
            AddColumn("dbo.Snack2", "servingSize", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Snack2", "servingSize");
            DropColumn("dbo.Snack1", "servingSize");
            DropColumn("dbo.Meal3", "servingSize");
            DropColumn("dbo.Meal2", "servingSize");
            DropColumn("dbo.Meal1", "servingSize");
        }
    }
}
