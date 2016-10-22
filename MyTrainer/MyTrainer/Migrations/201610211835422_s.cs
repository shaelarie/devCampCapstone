namespace MyTrainer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class s : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserPhotos", "Picture", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserPhotos", "Picture");
        }
    }
}
