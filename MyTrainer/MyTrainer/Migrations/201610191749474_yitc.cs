namespace MyTrainer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yitc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "BMI", c => c.Double());
            AddColumn("dbo.Users", "ProteinIntake", c => c.Double());
            AddColumn("dbo.Users", "FatIntake", c => c.Double());
            AddColumn("dbo.Users", "CarbIntake", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "CarbIntake");
            DropColumn("dbo.Users", "FatIntake");
            DropColumn("dbo.Users", "ProteinIntake");
            DropColumn("dbo.Users", "BMI");
        }
    }
}
