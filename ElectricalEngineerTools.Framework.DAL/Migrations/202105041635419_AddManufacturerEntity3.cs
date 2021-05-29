namespace ElectricalEngineerTools.Framework.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddManufacturerEntity3 : DbMigration
    {
        public override void Up()
        {
            /*RenameColumn(table: "dbo.LightingFixtures", name: "Manufacturer_Id", newName: "ManufacturerId");
            RenameIndex(table: "dbo.LightingFixtures", name: "IX_Manufacturer_Id", newName: "IX_ManufacturerId");*/
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.LightingFixtures", name: "IX_ManufacturerId", newName: "IX_Manufacturer_Id");
            RenameColumn(table: "dbo.LightingFixtures", name: "ManufacturerId", newName: "Manufacturer_Id");
        }
    }
}
