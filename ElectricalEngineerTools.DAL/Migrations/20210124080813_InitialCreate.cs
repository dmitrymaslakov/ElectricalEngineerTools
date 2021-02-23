using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElectricalEngineerTools.DAL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cables",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Brand = table.Column<string>(nullable: false),
                    CoresNumber = table.Column<int>(nullable: false),
                    Section = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LightingFixtures",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CableId = table.Column<Guid>(nullable: true),
                    Manufacturer = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(nullable: false),
                    Power = table.Column<double>(nullable: false),
                    ClimaticModification = table.Column<string>(nullable: false),
                    DiffuserMaterial = table.Column<string>(nullable: false),
                    IP = table.Column<string>(nullable: false),
                    IsFireproof = table.Column<bool>(nullable: false),
                    LdtIesFile = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LightingFixtures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LightingFixtures_Cables_CableId",
                        column: x => x.CableId,
                        principalTable: "Cables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LightingFixtures_CableId",
                table: "LightingFixtures",
                column: "CableId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LightingFixtures");

            migrationBuilder.DropTable(
                name: "Cables");
        }
    }
}
