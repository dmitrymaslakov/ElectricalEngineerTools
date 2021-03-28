namespace ElectricalEngineerTools.Framework.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cables",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Brand = c.String(nullable: false, unicode: false),
                    CoresNumber = c.Int(nullable: false),
                    Section = c.Double(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.LightingFixtures",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    CableId = c.Int(),
                    Manufacturer = c.String(unicode: false),
                    Brand = c.String(nullable: false, unicode: false),
                    Description = c.String(nullable: false, unicode: false),
                    Power = c.Double(nullable: false),
                    ClimaticModification = c.String(nullable: false, unicode: false),
                    DiffuserMaterial = c.String(nullable: false, unicode: false),
                    IP = c.String(nullable: false, unicode: false),
                    IsFireproof = c.Boolean(nullable: false),
                    BPSU = c.Boolean(nullable: false),
                    LightSourceType = c.String(nullable: false, unicode: false),
                    Length = c.String(nullable: false, unicode: false),
                    Width = c.String(nullable: false, unicode: false),

                    LdtIesFile = c.String(nullable: false, unicode: false),
                    DwgFile = c.String(nullable: false, unicode: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cables", t => t.CableId)
                .Index(t => t.CableId);

            CreateStoredProcedure(
                "dbo.Cable_Insert",
                p => new
                {
                    Brand = p.String(maxLength: 1073741823, unicode: false),
                    CoresNumber = p.Int(),
                    Section = p.Double(),
                },
                body:
                    @"SET SESSION sql_mode='ANSI';INSERT INTO `Cables`(
                      `Brand`, 
                      `CoresNumber`, 
                      `Section`) VALUES (
                      @Brand, 
                      @CoresNumber, 
                      @Section);
                      SELECT
                      `Id`
                      FROM `Cables`
                       WHERE  row_count() > 0 AND `Id`=last_insert_id();"
            );

            CreateStoredProcedure(
                "dbo.Cable_Update",
                p => new
                {
                    Id = p.Int(),
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
                    Id = p.Int(),
                },
                body:
                    @"DELETE FROM `Cables` WHERE `Id` = @Id;"
            );

            CreateStoredProcedure(
                "dbo.LightingFixture_Insert",
                p => new
                {
                    CableId = p.Int(),
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
                    Length = p.String(maxLength: 1073741823, unicode: false),
                    Width = p.String(maxLength: 1073741823, unicode: false),
                    LdtIesFile = p.String(maxLength: 1073741823, unicode: false),
                    DwgFile = p.String(maxLength: 1073741823, unicode: false),
                },
                body:
                    @"SET SESSION sql_mode='ANSI';INSERT INTO `LightingFixtures`(
                      `CableId`, 
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
                      `DwgFile`) VALUES (
                      @CableId, 
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
                      @DwgFile);
                      SELECT
                      `Id`
                      FROM `LightingFixtures`
                       WHERE  row_count() > 0 AND `Id`=last_insert_id();"
            );

            CreateStoredProcedure(
                "dbo.LightingFixture_Update",
                p => new
                {
                    Id = p.Int(),
                    CableId = p.Int(),
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
                    Length = p.String(maxLength: 1073741823, unicode: false),
                    Width = p.String(maxLength: 1073741823, unicode: false),
                    LdtIesFile = p.String(maxLength: 1073741823, unicode: false),
                    DwgFile = p.String(maxLength: 1073741823, unicode: false),
                },
                body:
                    @"UPDATE `LightingFixtures` SET `CableId`=@CableId, `Manufacturer`=@Manufacturer, `Brand`=@Brand, `Description`=@Description, `Power`=@Power, `ClimaticModification`=@ClimaticModification, `DiffuserMaterial`=@DiffuserMaterial, `IP`=@IP, `IsFireproof`=@IsFireproof, `BPSU`=@BPSU, `LightSourceType`=@LightSourceType, `Length`=@Length, `Width`=@Width, `LdtIesFile`=@LdtIesFile, `DwgFile`=@DwgFile WHERE `Id` = @Id;"
            );

            CreateStoredProcedure(
                "dbo.LightingFixture_Delete",
                p => new
                {
                    Id = p.Int(),
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
            DropForeignKey("dbo.LightingFixtures", "CableId", "dbo.Cables");
            DropIndex("dbo.LightingFixtures", new[] { "CableId" });
            DropTable("dbo.LightingFixtures");
            DropTable("dbo.Cables");
        }
    }
}
