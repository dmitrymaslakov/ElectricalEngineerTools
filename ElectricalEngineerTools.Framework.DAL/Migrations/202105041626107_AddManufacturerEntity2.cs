namespace ElectricalEngineerTools.Framework.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddManufacturerEntity2 : DbMigration
    {
        public override void Up()
        {
            /*CreateTable(
    "dbo.Manufacturers",
    c => new
    {
        Id = c.Int(nullable: false, identity: true),
        Name = c.String(nullable: false, unicode: false),
    })
    .PrimaryKey(t => t.Id);

            CreateIndex("dbo.LightingFixtures", "ManufacturerId");*/

            AddForeignKey("dbo.LightingFixtures", "ManufacturerId", "dbo.Manufacturers", "Id", cascadeDelete: true);
        }

        public override void Down()
        {
        }
    }
}
