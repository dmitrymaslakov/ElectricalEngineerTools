namespace ElectricalEngineerTools.Framework.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BoolToInt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LightingFixtures", "IsFireproof", c => c.Int(nullable: false));
            AlterStoredProcedure(
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
                        IsFireproof = p.Int(),
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
            
            AlterStoredProcedure(
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
                        IsFireproof = p.Int(),
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
            
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LightingFixtures", "IsFireproof", c => c.Boolean(nullable: false));
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
