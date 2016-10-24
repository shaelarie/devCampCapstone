namespace MyTrainer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class iygvh8ivrs : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Meal1", "servingSize", c => c.Double());
            AlterColumn("dbo.Meal2", "servingSize", c => c.Double());
            AlterColumn("dbo.Meal3", "servingSize", c => c.Double());
            AlterColumn("dbo.Snack1", "servingSize", c => c.Double());
            AlterColumn("dbo.Snack2", "servingSize", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Snack2", "servingSize", c => c.Double(nullable: false));
            AlterColumn("dbo.Snack1", "servingSize", c => c.Double(nullable: false));
            AlterColumn("dbo.Meal3", "servingSize", c => c.Double(nullable: false));
            AlterColumn("dbo.Meal2", "servingSize", c => c.Double(nullable: false));
            AlterColumn("dbo.Meal1", "servingSize", c => c.Double(nullable: false));
        }
    }
}
