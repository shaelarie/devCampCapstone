namespace MyTrainer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ytguh : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Chatrooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        messages = c.String(),
                        name = c.String(),
                        userId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.userId, cascadeDelete: true)
                .Index(t => t.userId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        age = c.Int(),
                        Username = c.String(),
                        Weight = c.Int(),
                        HeightFt = c.Int(),
                        HeightIn = c.Int(),
                        TDEE = c.Double(),
                        BMR = c.Double(),
                        WorkoutAmount = c.Int(),
                        DailyCalorieIntake = c.Double(),
                        BMI = c.Double(),
                        ProteinIntake = c.Double(),
                        FatIntake = c.Double(),
                        CarbIntake = c.Double(),
                        Gender = c.String(),
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
                "dbo.Goals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserGoal = c.String(),
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
                        mealPlanDetails = c.String(),
                        CalorieIntake = c.Double(),
                        ProteinIntake = c.Double(),
                        CarbIntake = c.Double(),
                        FatIntake = c.Double(),
                        CaloriesAdded = c.Double(),
                        ProteinAdded = c.Double(),
                        CarbsAdded = c.Double(),
                        FatAdded = c.Double(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Meal1",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FoodItem = c.String(),
                        servingSize = c.Double(),
                        calories = c.Double(),
                        protein = c.Double(),
                        fat = c.Double(),
                        carbs = c.Double(),
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
                        servingSize = c.Double(),
                        calories = c.Double(),
                        protein = c.Double(),
                        fat = c.Double(),
                        carbs = c.Double(),
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
                        servingSize = c.Double(),
                        calories = c.Double(),
                        protein = c.Double(),
                        fat = c.Double(),
                        carbs = c.Double(),
                        MealPlanId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MealPlans", t => t.MealPlanId, cascadeDelete: true)
                .Index(t => t.MealPlanId);
            
            CreateTable(
                "dbo.UserPhotos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PictureDescription = c.String(),
                        DateTaken = c.DateTime(),
                        Picture = c.String(),
                        FileName = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
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
                "dbo.Snack1",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FoodItem = c.String(),
                        servingSize = c.Double(),
                        calories = c.Double(),
                        protein = c.Double(),
                        fat = c.Double(),
                        carbs = c.Double(),
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
                        servingSize = c.Double(),
                        calories = c.Double(),
                        protein = c.Double(),
                        fat = c.Double(),
                        carbs = c.Double(),
                        MealPlanId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MealPlans", t => t.MealPlanId, cascadeDelete: true)
                .Index(t => t.MealPlanId);
            
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
            DropForeignKey("dbo.Snack2", "MealPlanId", "dbo.MealPlans");
            DropForeignKey("dbo.Snack1", "MealPlanId", "dbo.MealPlans");
            DropForeignKey("dbo.UserSchedules", "UserId", "dbo.Users");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.UserPhotos", "UserId", "dbo.Users");
            DropForeignKey("dbo.Meal3", "MealPlanId", "dbo.MealPlans");
            DropForeignKey("dbo.Meal2", "MealPlanId", "dbo.MealPlans");
            DropForeignKey("dbo.Meal1", "MealPlanId", "dbo.MealPlans");
            DropForeignKey("dbo.Chatrooms", "userId", "dbo.Users");
            DropForeignKey("dbo.Users", "MealPlanId", "dbo.MealPlans");
            DropForeignKey("dbo.Users", "GoalId", "dbo.Goals");
            DropForeignKey("dbo.WorkoutsGoals", "Goals_Id", "dbo.Goals");
            DropForeignKey("dbo.WorkoutsGoals", "Workouts_Id", "dbo.Workouts");
            DropForeignKey("dbo.Users", "LoginId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.WorkoutsGoals", new[] { "Goals_Id" });
            DropIndex("dbo.WorkoutsGoals", new[] { "Workouts_Id" });
            DropIndex("dbo.Snack2", new[] { "MealPlanId" });
            DropIndex("dbo.Snack1", new[] { "MealPlanId" });
            DropIndex("dbo.UserSchedules", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.UserPhotos", new[] { "UserId" });
            DropIndex("dbo.Meal3", new[] { "MealPlanId" });
            DropIndex("dbo.Meal2", new[] { "MealPlanId" });
            DropIndex("dbo.Meal1", new[] { "MealPlanId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Users", new[] { "MealPlanId" });
            DropIndex("dbo.Users", new[] { "GoalId" });
            DropIndex("dbo.Users", new[] { "LoginId" });
            DropIndex("dbo.Chatrooms", new[] { "userId" });
            DropTable("dbo.WorkoutsGoals");
            DropTable("dbo.Snack2");
            DropTable("dbo.Snack1");
            DropTable("dbo.UserSchedules");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.UserPhotos");
            DropTable("dbo.Meal3");
            DropTable("dbo.Meal2");
            DropTable("dbo.Meal1");
            DropTable("dbo.MealPlans");
            DropTable("dbo.Workouts");
            DropTable("dbo.Goals");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Users");
            DropTable("dbo.Chatrooms");
        }
    }
}
