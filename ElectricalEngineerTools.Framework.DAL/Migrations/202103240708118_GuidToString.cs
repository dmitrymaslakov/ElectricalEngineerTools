namespace ElectricalEngineerTools.Framework.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GuidToString : DbMigration
    {
        public override void Up()
        {

            /*DropForeignKey("LightingFixtures", "CableId", "Cables");
            DropIndex("LightingFixtures", new[] { "CableId" });
            DropPrimaryKey("Cables");
            DropPrimaryKey("LightingFixtures");
            AddColumn("LightingFixtures", "Cable_Id", c => c.String(maxLength: 128, storeType: "nvarchar"));
            AlterColumn("Cables", "Id", c => c.String(nullable: false, maxLength: 128, storeType: "nvarchar"));
            AlterColumn("LightingFixtures", "Id", c => c.String(nullable: false, maxLength: 128, storeType: "nvarchar"));
            AddPrimaryKey("Cables", "Id");
            AddPrimaryKey("LightingFixtures", "Id");
            CreateIndex("LightingFixtures", "Cable_Id");
            AddForeignKey("LightingFixtures", "Cable_Id", "Cables", "Id");
            CreateStoredProcedure(
                "dbo.Cable_Insert",
                p => new
                    {
                        Id = p.String(maxLength: 128, unicode: false, storeType: "nvarchar"),
                        Brand = p.String(maxLength: 1073741823, unicode: false),
                        CoresNumber = p.Int(),
                        Section = p.Double(),
                    },
                body:
                    @"INSERT INTO `Cables`(
                      `Id`, 
                      `Brand`, 
                      `CoresNumber`, 
                      `Section`) VALUES (
                      @Id, 
                      @Brand, 
                      @CoresNumber, 
                      @Section);"
            );

            CreateStoredProcedure(
                "dbo.Cable_Update",
                p => new
                    {
                        Id = p.String(maxLength: 128, unicode: false, storeType: "nvarchar"),
                        Brand = p.String(maxLength: 1073741823, unicode: false),
                        CoresNumber = p.Int(),
                        Section = p.Double(),
                    },
                body:
                    @"UPDATE `Cables` SET `Brand`=@Brand, `CoresNumber`=@CoresNumber, `Section`=@Section WHERE `Id` = @Id;"
            );

            CreateStoredProcedure(
                "dbo.Cable_Delete",
                p => new
                    {
                        Id = p.String(maxLength: 128, unicode: false, storeType: "nvarchar"),
                    },
                body:
                    @"DELETE FROM `Cables` WHERE `Id` = @Id;"
            );

            CreateStoredProcedure(
                "dbo.LightingFixture_Insert",
                p => new
                    {
                        Id = p.String(maxLength: 128, unicode: false, storeType: "nvarchar"),
                        Manufacturer = p.String(maxLength: 1073741823, unicode: false),
                        Brand = p.String(maxLength: 1073741823, unicode: false),
                        Description = p.String(maxLength: 1073741823, unicode: false),
                        Power = p.Double(),
                        ClimaticModification = p.String(maxLength: 1073741823, unicode: false),
                        DiffuserMaterial = p.String(maxLength: 1073741823, unicode: false),
                        IP = p.String(maxLength: 1073741823, unicode: false),
                        IsFireproof = p.Boolean(),
                        BPSU = p.Boolean(),
                        LightSourceType = p.String(maxLength: 1073741823, unicode: false),
                        Length = p.Double(),
                        Width = p.Double(),
                        LdtIesFile = p.String(maxLength: 1073741823, unicode: false),
                        DwgFile = p.String(maxLength: 1073741823, unicode: false),
                        CableId = p.Guid(),
                        Cable_Id = p.String(maxLength: 128, unicode: false, storeType: "nvarchar"),
                    },
                body:
                    @"INSERT INTO `LightingFixtures`(
                      `Id`, 
                      `Manufacturer`, 
                      `Brand`, 
                      `Description`, 
                      `Power`, 
                      `ClimaticModification`, 
                      `DiffuserMaterial`, 
                      `IP`, 
                      `IsFireproof`, 
                      `BPSU`, 
                      `LightSourceType`, 
                      `Length`, 
                      `Width`, 
                      `LdtIesFile`, 
                      `DwgFile`, 
                      `CableId`, 
                      `Cable_Id`) VALUES (
                      @Id, 
                      @Manufacturer, 
                      @Brand, 
                      @Description, 
                      @Power, 
                      @ClimaticModification, 
                      @DiffuserMaterial, 
                      @IP, 
                      @IsFireproof, 
                      @BPSU, 
                      @LightSourceType, 
                      @Length, 
                      @Width, 
                      @LdtIesFile, 
                      @DwgFile, 
                      @CableId, 
                      @Cable_Id);"
            );

            CreateStoredProcedure(
                "dbo.LightingFixture_Update",
                p => new
                    {
                        Id = p.String(maxLength: 128, unicode: false, storeType: "nvarchar"),
                        Manufacturer = p.String(maxLength: 1073741823, unicode: false),
                        Brand = p.String(maxLength: 1073741823, unicode: false),
                        Description = p.String(maxLength: 1073741823, unicode: false),
                        Power = p.Double(),
                        ClimaticModification = p.String(maxLength: 1073741823, unicode: false),
                        DiffuserMaterial = p.String(maxLength: 1073741823, unicode: false),
                        IP = p.String(maxLength: 1073741823, unicode: false),
                        IsFireproof = p.Boolean(),
                        BPSU = p.Boolean(),
                        LightSourceType = p.String(maxLength: 1073741823, unicode: false),
                        Length = p.Double(),
                        Width = p.Double(),
                        LdtIesFile = p.String(maxLength: 1073741823, unicode: false),
                        DwgFile = p.String(maxLength: 1073741823, unicode: false),
                        CableId = p.Guid(),
                        Cable_Id = p.String(maxLength: 128, unicode: false, storeType: "nvarchar"),
                    },
                body:
                    @"UPDATE `LightingFixtures` SET `Manufacturer`=@Manufacturer, `Brand`=@Brand, `Description`=@Description, `Power`=@Power, `ClimaticModification`=@ClimaticModification, `DiffuserMaterial`=@DiffuserMaterial, `IP`=@IP, `IsFireproof`=@IsFireproof, `BPSU`=@BPSU, `LightSourceType`=@LightSourceType, `Length`=@Length, `Width`=@Width, `LdtIesFile`=@LdtIesFile, `DwgFile`=@DwgFile, `CableId`=@CableId, `Cable_Id`=@Cable_Id WHERE `Id` = @Id;"
            );

            CreateStoredProcedure(
                "dbo.LightingFixture_Delete",
                p => new
                    {
                        Id = p.String(maxLength: 128, unicode: false, storeType: "nvarchar"),
                        Cable_Id = p.String(maxLength: 128, unicode: false, storeType: "nvarchar"),
                    },
                body:
                    @"DELETE FROM `LightingFixtures` WHERE (`Id` = @Id) AND ((`Cable_Id` = @Cable_Id) OR ((`Cable_Id` IS  NULL) AND (@Cable_Id IS  NULL)));"
            );*/
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("LightingFixture_Delete");
            DropStoredProcedure("LightingFixture_Update");
            DropStoredProcedure("LightingFixture_Insert");
            DropStoredProcedure("Cable_Delete");
            DropStoredProcedure("Cable_Update");
            DropStoredProcedure("Cable_Insert");
            DropForeignKey("LightingFixtures", "Cable_Id", "Cables");
            DropIndex("LightingFixtures", new[] { "Cable_Id" });
            DropPrimaryKey("LightingFixtures");
            DropPrimaryKey("Cables");
            AlterColumn("LightingFixtures", "Id", c => c.Guid(nullable: false));
            AlterColumn("Cables", "Id", c => c.Guid(nullable: false));
            DropColumn("LightingFixtures", "Cable_Id");
            AddPrimaryKey("LightingFixtures", "Id");
            AddPrimaryKey("Cables", "Id");
            CreateIndex("LightingFixtures", "CableId");
            AddForeignKey("LightingFixtures", "CableId", "Cables", "Id");
            //throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
