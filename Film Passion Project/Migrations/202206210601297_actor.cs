namespace Film_Passion_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class actor : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Actors", "Film_FilmId", "dbo.Films");
            DropForeignKey("dbo.Actors", "FilmId", "dbo.Films");
            DropForeignKey("dbo.Films", "Actor_ActorId", "dbo.Actors");
            DropIndex("dbo.Actors", new[] { "FilmId" });
            DropIndex("dbo.Actors", new[] { "Film_FilmId" });
            DropIndex("dbo.Films", new[] { "Actor_ActorId" });
            CreateTable(
                "dbo.FilmActors",
                c => new
                    {
                        Film_FilmId = c.Int(nullable: false),
                        Actor_ActorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Film_FilmId, t.Actor_ActorId })
                .ForeignKey("dbo.Films", t => t.Film_FilmId, cascadeDelete: true)
                .ForeignKey("dbo.Actors", t => t.Actor_ActorId, cascadeDelete: true)
                .Index(t => t.Film_FilmId)
                .Index(t => t.Actor_ActorId);
            
            DropColumn("dbo.Actors", "FilmId");
            DropColumn("dbo.Actors", "Film_FilmId");
            DropColumn("dbo.Films", "Actor_ActorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Films", "Actor_ActorId", c => c.Int());
            AddColumn("dbo.Actors", "Film_FilmId", c => c.Int());
            AddColumn("dbo.Actors", "FilmId", c => c.Int(nullable: false));
            DropForeignKey("dbo.FilmActors", "Actor_ActorId", "dbo.Actors");
            DropForeignKey("dbo.FilmActors", "Film_FilmId", "dbo.Films");
            DropIndex("dbo.FilmActors", new[] { "Actor_ActorId" });
            DropIndex("dbo.FilmActors", new[] { "Film_FilmId" });
            DropTable("dbo.FilmActors");
            CreateIndex("dbo.Films", "Actor_ActorId");
            CreateIndex("dbo.Actors", "Film_FilmId");
            CreateIndex("dbo.Actors", "FilmId");
            AddForeignKey("dbo.Films", "Actor_ActorId", "dbo.Actors", "ActorId");
            AddForeignKey("dbo.Actors", "FilmId", "dbo.Films", "FilmId", cascadeDelete: true);
            AddForeignKey("dbo.Actors", "Film_FilmId", "dbo.Films", "FilmId");
        }
    }
}
