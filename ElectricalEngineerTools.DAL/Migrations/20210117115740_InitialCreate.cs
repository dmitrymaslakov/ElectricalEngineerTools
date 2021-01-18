using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElectricalEngineerTools.DAL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LightingFixtures",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Manufacturer = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    Power = table.Column<double>(nullable: false),
                    ClimaticModification = table.Column<string>(nullable: true),
                    DiffuserMaterial = table.Column<string>(nullable: true),
                    IP = table.Column<string>(nullable: true),
                    IsFireproof = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LightingFixtures", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LightingFixtures");
        }
    }
}
