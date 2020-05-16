using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class PocoFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionTreeId",
                table: "Scenarios");

            migrationBuilder.DropColumn(
                name: "ScenarioId",
                table: "Questions");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Scenarios",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "QuestionTrees",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuestionTreeId",
                table: "Questions",
                column: "QuestionTreeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_QuestionTrees_QuestionTreeId",
                table: "Questions",
                column: "QuestionTreeId",
                principalTable: "QuestionTrees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_QuestionTrees_QuestionTreeId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_QuestionTreeId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Scenarios");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "QuestionTrees");

            migrationBuilder.AddColumn<Guid>(
                name: "QuestionTreeId",
                table: "Scenarios",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ScenarioId",
                table: "Questions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
