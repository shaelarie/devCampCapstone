namespace MyTrainer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class qreswe : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Goals", "UserGoal", c => c.String());
            DropColumn("dbo.Goals", "LoseWeight");
            DropColumn("dbo.Goals", "GainMuscle");
            DropColumn("dbo.Goals", "Maintain");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Goals", "Maintain", c => c.Boolean(nullable: false));
            AddColumn("dbo.Goals", "GainMuscle", c => c.Boolean(nullable: false));
            AddColumn("dbo.Goals", "LoseWeight", c => c.Boolean(nullable: false));
            DropColumn("dbo.Goals", "UserGoal");
        }
    }
}
