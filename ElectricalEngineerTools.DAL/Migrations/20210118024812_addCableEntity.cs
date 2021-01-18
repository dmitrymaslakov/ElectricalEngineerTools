using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElectricalEngineerTools.DAL.Migrations
{
    public partial class addCableEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IP",
                table: "LightingFixtures",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DiffuserMaterial",
                table: "LightingFixtures",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClimaticModification",
                table: "LightingFixtures",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Brand",
                table: "LightingFixtures",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Cables",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Brand = table.Column<string>(nullable: false),
                    CoresNumber = table.Column<int>(nullable: false),
                    Section = table.Column<double>(nullable: false),
                    LightingFixtureId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cables_LightingFixtures_LightingFixtureId",
                        column: x => x.LightingFixtureId,
                        principalTable: "LightingFixtures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cables_LightingFixtureId",
                table: "Cables",
                column: "LightingFixtureId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cables");

            migrationBuilder.AlterColumn<string>(
                name: "IP",
                table: "LightingFixtures",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "DiffuserMaterial",
                table: "LightingFixtures",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ClimaticModification",
                table: "LightingFixtures",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Brand",
                table: "LightingFixtures",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
