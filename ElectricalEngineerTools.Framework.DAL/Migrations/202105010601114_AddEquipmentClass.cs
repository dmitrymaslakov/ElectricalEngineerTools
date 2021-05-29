namespace ElectricalEngineerTools.Framework.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEquipmentClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LightingFixtures", "EquipmentClass", c => c.String(nullable: false, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LightingFixtures", "EquipmentClass");
        }
    }
}
