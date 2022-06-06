namespace Film_Passion_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class actors : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actors",
                c => new
                    {
                        ActorId = c.Int(nullable: false, identity: true),
                        ActorName = c.String(),
                        ActorFee = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ActorId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Actors");
        }
    }
}
