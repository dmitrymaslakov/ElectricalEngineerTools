namespace ElectricalEngineerTools.Framework.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GuidToInt : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.LightingFixtures", "CableId", "dbo.Cables");
            /*DropIndex("dbo.LightingFixtures", new[] { "CableId" });*/
            /*DropPrimaryKey("dbo.LightingFixtures");
            AlterColumn("dbo.Cables", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.LightingFixtures", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.LightingFixtures", "CableId", c => c.Int());
            CreateIndex("dbo.LightingFixtures", "CableId");*/
            AddForeignKey("LightingFixtures", "CableId", "Cables", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LightingFixtures", "CableId", "dbo.Cables");
            DropIndex("dbo.LightingFixtures", new[] { "CableId" });
            DropPrimaryKey("dbo.LightingFixtures");
            DropPrimaryKey("dbo.Cables");
            AlterColumn("dbo.LightingFixtures", "CableId", c => c.Guid());
            AlterColumn("dbo.LightingFixtures", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Cables", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.LightingFixtures", "Id");
            AddPrimaryKey("dbo.Cables", "Id");
            CreateIndex("dbo.LightingFixtures", "CableId");
            AddForeignKey("dbo.LightingFixtures", "CableId", "dbo.Cables", "Id");
        }
    }
}
