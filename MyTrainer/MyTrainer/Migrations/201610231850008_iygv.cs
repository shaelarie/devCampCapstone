namespace MyTrainer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class iygv : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Meal1",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FoodItem = c.String(),
                        MealPlanId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MealPlans", t => t.MealPlanId, cascadeDelete: true)
                .Index(t => t.MealPlanId);
            
            CreateTable(
                "dbo.Meal2",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FoodItem = c.String(),
                        MealPlanId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MealPlans", t => t.MealPlanId, cascadeDelete: true)
                .Index(t => t.MealPlanId);
            
            CreateTable(
                "dbo.Meal3",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FoodItem = c.String(),
                        MealPlanId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MealPlans", t => t.MealPlanId, cascadeDelete: true)
                .Index(t => t.MealPlanId);
            
            CreateTable(
                "dbo.Snack1",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FoodItem = c.String(),
                        MealPlanId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MealPlans", t => t.MealPlanId, cascadeDelete: true)
                .Index(t => t.MealPlanId);
            
            CreateTable(
                "dbo.Snack2",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FoodItem = c.String(),
                        MealPlanId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MealPlans", t => t.MealPlanId, cascadeDelete: true)
                .Index(t => t.MealPlanId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Snack2", "MealPlanId", "dbo.MealPlans");
            DropForeignKey("dbo.Snack1", "MealPlanId", "dbo.MealPlans");
            DropForeignKey("dbo.Meal3", "MealPlanId", "dbo.MealPlans");
            DropForeignKey("dbo.Meal2", "MealPlanId", "dbo.MealPlans");
            DropForeignKey("dbo.Meal1", "MealPlanId", "dbo.MealPlans");
            DropIndex("dbo.Snack2", new[] { "MealPlanId" });
            DropIndex("dbo.Snack1", new[] { "MealPlanId" });
            DropIndex("dbo.Meal3", new[] { "MealPlanId" });
            DropIndex("dbo.Meal2", new[] { "MealPlanId" });
            DropIndex("dbo.Meal1", new[] { "MealPlanId" });
            DropTable("dbo.Snack2");
            DropTable("dbo.Snack1");
            DropTable("dbo.Meal3");
            DropTable("dbo.Meal2");
            DropTable("dbo.Meal1");
        }
    }
}
