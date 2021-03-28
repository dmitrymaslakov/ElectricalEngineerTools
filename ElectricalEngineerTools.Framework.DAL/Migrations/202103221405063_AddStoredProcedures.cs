namespace ElectricalEngineerTools.Framework.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStoredProcedures : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LightingFixtures", "Length", c => c.Double(nullable: false));
            AlterColumn("dbo.LightingFixtures", "Width", c => c.Double(nullable: false));
            CreateStoredProcedure(
                "dbo.Cable_Insert",
                p => new
                    {
                        Id = p.Guid(),
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
                        Id = p.Guid(),
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
                        Id = p.Guid(),
                    },
                body:
                    @"DELETE FROM `Cables` WHERE `Id` = @Id;"
            );
            
            CreateStoredProcedure(
                "dbo.LightingFixture_Insert",
                p => new
                    {
                        Id = p.Guid(),
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
                      `CableId`) VALUES (
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
                      @CableId);"
            );
            
            CreateStoredProcedure(
                "dbo.LightingFixture_Update",
                p => new
                    {
                        Id = p.Guid(),
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
                    },
                body:
                    @"UPDATE `LightingFixtures` SET `Manufacturer`=@Manufacturer, `Brand`=@Brand, `Description`=@Description, `Power`=@Power, `ClimaticModification`=@ClimaticModification, `DiffuserMaterial`=@DiffuserMaterial, `IP`=@IP, `IsFireproof`=@IsFireproof, `BPSU`=@BPSU, `LightSourceType`=@LightSourceType, `Length`=@Length, `Width`=@Width, `LdtIesFile`=@LdtIesFile, `DwgFile`=@DwgFile, `CableId`=@CableId WHERE `Id` = @Id;"
            );
            
            CreateStoredProcedure(
                "dbo.LightingFixture_Delete",
                p => new
                    {
                        Id = p.Guid(),
                    },
                body:
                    @"DELETE FROM `LightingFixtures` WHERE `Id` = @Id;"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.LightingFixture_Delete");
            DropStoredProcedure("dbo.LightingFixture_Update");
            DropStoredProcedure("dbo.LightingFixture_Insert");
            DropStoredProcedure("dbo.Cable_Delete");
            DropStoredProcedure("dbo.Cable_Update");
            DropStoredProcedure("dbo.Cable_Insert");
            AlterColumn("dbo.LightingFixtures", "Width", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.LightingFixtures", "Length", c => c.String(nullable: false, unicode: false));
        }
    }
}
