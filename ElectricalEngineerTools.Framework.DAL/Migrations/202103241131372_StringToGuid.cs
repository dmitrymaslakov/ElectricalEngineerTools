namespace ElectricalEngineerTools.Framework.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StringToGuid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("LightingFixtures", "CableId", "Cables");
            DropIndex("LightingFixtures", new[] { "CableId" });
            DropPrimaryKey("Cables");
            DropPrimaryKey("LightingFixtures");
            AlterColumn("Cables", "Id", c => c.Guid(nullable: false));
            AlterColumn("LightingFixtures", "Id", c => c.Guid(nullable: false));
            AlterColumn("LightingFixtures", "CableId", c => c.Guid());
            AddPrimaryKey("Cables", "Id");
            AddPrimaryKey("LightingFixtures", "Id");
            CreateIndex("LightingFixtures", "CableId");
            AddForeignKey("LightingFixtures", "CableId", "Cables", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("LightingFixtures", "CableId", "Cables");
            DropIndex("LightingFixtures", new[] { "CableId" });
            DropPrimaryKey("LightingFixtures");
            DropPrimaryKey("Cables");
            AlterColumn("LightingFixtures", "CableId", c => c.String(maxLength: 128, storeType: "nvarchar"));
            AlterColumn("LightingFixtures", "Id", c => c.String(nullable: false, maxLength: 128, storeType: "nvarchar"));
            AlterColumn("Cables", "Id", c => c.String(nullable: false, maxLength: 128, storeType: "nvarchar"));
            AddPrimaryKey("LightingFixtures", "Id");
            AddPrimaryKey("Cables", "Id");
            CreateIndex("LightingFixtures", "CableId");
            AddForeignKey("LightingFixtures", "CableId", "Cables", "Id");
        }
    }
}
