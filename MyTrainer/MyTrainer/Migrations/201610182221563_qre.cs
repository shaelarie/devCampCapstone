namespace MyTrainer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class qre : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BasicMealPlans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProteinIntake = c.Double(),
                        CarbIntake = c.Double(),
                        FatIntake = c.Double(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Goals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LoseWeight = c.Boolean(nullable: false),
                        GainMuscle = c.Boolean(nullable: false),
                        Maintain = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Workouts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WorkoutName = c.String(),
                        Description = c.String(),
                        NumberOfDays = c.Int(nullable: false),
                        Reps = c.Int(nullable: false),
                        Sets = c.Int(nullable: false),
                        Distance = c.Double(nullable: false),
                        CaloriesBurned = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MealPlans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MealPlanType = c.String(),
                        mealPlanDetails = c.String(),
                        VegetarianId = c.Int(),
                        VeganId = c.Int(),
                        BasicId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BasicMealPlans", t => t.BasicId)
                .ForeignKey("dbo.VeganMealPlans", t => t.VeganId)
                .ForeignKey("dbo.VegetarianMealPlans", t => t.VegetarianId)
                .Index(t => t.VegetarianId)
                .Index(t => t.VeganId)
                .Index(t => t.BasicId);
            
            CreateTable(
                "dbo.VeganMealPlans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProteinIntake = c.Double(),
                        CarbIntake = c.Double(),
                        FatIntake = c.Double(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VegetarianMealPlans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProteinIntake = c.Double(),
                        CarbIntake = c.Double(),
                        FatIntake = c.Double(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserPhotos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTaken = c.DateTime(nullable: false),
                        Picture = c.Binary(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Weight = c.Int(),
                        HeightFt = c.Int(),
                        HeightIn = c.Int(),
                        LoginId = c.String(maxLength: 128),
                        GoalId = c.Int(nullable: false),
                        MealPlanId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.LoginId)
                .ForeignKey("dbo.Goals", t => t.GoalId, cascadeDelete: true)
                .ForeignKey("dbo.MealPlans", t => t.MealPlanId, cascadeDelete: true)
                .Index(t => t.LoginId)
                .Index(t => t.GoalId)
                .Index(t => t.MealPlanId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.UserSchedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        eventId = c.String(),
                        title = c.String(),
                        eventDescription = c.String(),
                        start = c.String(),
                        end = c.String(),
                        startTime = c.String(),
                        endTime = c.String(),
                        background = c.String(),
                        editable = c.Boolean(nullable: false),
                        startDate = c.DateTime(),
                        endDate = c.DateTime(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.WorkoutsGoals",
                c => new
                    {
                        Workouts_Id = c.Int(nullable: false),
                        Goals_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Workouts_Id, t.Goals_Id })
                .ForeignKey("dbo.Workouts", t => t.Workouts_Id, cascadeDelete: true)
                .ForeignKey("dbo.Goals", t => t.Goals_Id, cascadeDelete: true)
                .Index(t => t.Workouts_Id)
                .Index(t => t.Goals_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSchedules", "UserId", "dbo.Users");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.UserPhotos", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "MealPlanId", "dbo.MealPlans");
            DropForeignKey("dbo.Users", "GoalId", "dbo.Goals");
            DropForeignKey("dbo.Users", "LoginId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.MealPlans", "VegetarianId", "dbo.VegetarianMealPlans");
            DropForeignKey("dbo.MealPlans", "VeganId", "dbo.VeganMealPlans");
            DropForeignKey("dbo.MealPlans", "BasicId", "dbo.BasicMealPlans");
            DropForeignKey("dbo.WorkoutsGoals", "Goals_Id", "dbo.Goals");
            DropForeignKey("dbo.WorkoutsGoals", "Workouts_Id", "dbo.Workouts");
            DropIndex("dbo.WorkoutsGoals", new[] { "Goals_Id" });
            DropIndex("dbo.WorkoutsGoals", new[] { "Workouts_Id" });
            DropIndex("dbo.UserSchedules", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Users", new[] { "MealPlanId" });
            DropIndex("dbo.Users", new[] { "GoalId" });
            DropIndex("dbo.Users", new[] { "LoginId" });
            DropIndex("dbo.UserPhotos", new[] { "UserId" });
            DropIndex("dbo.MealPlans", new[] { "BasicId" });
            DropIndex("dbo.MealPlans", new[] { "VeganId" });
            DropIndex("dbo.MealPlans", new[] { "VegetarianId" });
            DropTable("dbo.WorkoutsGoals");
            DropTable("dbo.UserSchedules");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Users");
            DropTable("dbo.UserPhotos");
            DropTable("dbo.VegetarianMealPlans");
            DropTable("dbo.VeganMealPlans");
            DropTable("dbo.MealPlans");
            DropTable("dbo.Workouts");
            DropTable("dbo.Goals");
            DropTable("dbo.BasicMealPlans");
        }
    }
}