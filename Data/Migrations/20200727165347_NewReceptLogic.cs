using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class NewReceptLogic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Template",
                table: "Recepts");

            migrationBuilder.AddColumn<bool>(
                name: "ByAge",
                table: "Recepts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ByWeight",
                table: "Recepts",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MedcinId",
                table: "Recepts",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReceptTemplateId",
                table: "Recepts",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ScenarioId",
                table: "Recepts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ReceptTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Template = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceptTemplates", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceptTemplates");

            migrationBuilder.DropColumn(
                name: "ByAge",
                table: "Recepts");

            migrationBuilder.DropColumn(
                name: "ByWeight",
                table: "Recepts");

            migrationBuilder.DropColumn(
                name: "MedcinId",
                table: "Recepts");

            migrationBuilder.DropColumn(
                name: "ReceptTemplateId",
                table: "Recepts");

            migrationBuilder.DropColumn(
                name: "ScenarioId",
                table: "Recepts");

            migrationBuilder.AddColumn<string>(
                name: "Template",
                table: "Recepts",
                type: "text",
                nullable: true);
        }
    }
}
