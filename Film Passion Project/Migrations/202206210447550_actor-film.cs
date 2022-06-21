namespace Film_Passion_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class actorfilm : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FilmActors", "Film_FilmId", "dbo.Films");
            DropForeignKey("dbo.FilmActors", "Actor_ActorId", "dbo.Actors");
            DropIndex("dbo.FilmActors", new[] { "Film_FilmId" });
            DropIndex("dbo.FilmActors", new[] { "Actor_ActorId" });
            AddColumn("dbo.Actors", "FilmId", c => c.Int(nullable: false));
            AddColumn("dbo.Actors", "Film_FilmId", c => c.Int());
            AddColumn("dbo.Films", "Actor_ActorId", c => c.Int());
            CreateIndex("dbo.Actors", "FilmId");
            CreateIndex("dbo.Actors", "Film_FilmId");
            CreateIndex("dbo.Films", "Actor_ActorId");
            AddForeignKey("dbo.Actors", "Film_FilmId", "dbo.Films", "FilmId");
            AddForeignKey("dbo.Actors", "FilmId", "dbo.Films", "FilmId", cascadeDelete: true);
            AddForeignKey("dbo.Films", "Actor_ActorId", "dbo.Actors", "ActorId");
            DropTable("dbo.FilmActors");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FilmActors",
                c => new
                    {
                        Film_FilmId = c.Int(nullable: false),
                        Actor_ActorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Film_FilmId, t.Actor_ActorId });
            
            DropForeignKey("dbo.Films", "Actor_ActorId", "dbo.Actors");
            DropForeignKey("dbo.Actors", "FilmId", "dbo.Films");
            DropForeignKey("dbo.Actors", "Film_FilmId", "dbo.Films");
            DropIndex("dbo.Films", new[] { "Actor_ActorId" });
            DropIndex("dbo.Actors", new[] { "Film_FilmId" });
            DropIndex("dbo.Actors", new[] { "FilmId" });
            DropColumn("dbo.Films", "Actor_ActorId");
            DropColumn("dbo.Actors", "Film_FilmId");
            DropColumn("dbo.Actors", "FilmId");
            CreateIndex("dbo.FilmActors", "Actor_ActorId");
            CreateIndex("dbo.FilmActors", "Film_FilmId");
            AddForeignKey("dbo.FilmActors", "Actor_ActorId", "dbo.Actors", "ActorId", cascadeDelete: true);
            AddForeignKey("dbo.FilmActors", "Film_FilmId", "dbo.Films", "FilmId", cascadeDelete: true);
        }
    }
}
