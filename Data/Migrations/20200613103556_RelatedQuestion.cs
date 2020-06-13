using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class RelatedQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Params_ParamId",
                table: "Questions");

            migrationBuilder.AlterColumn<Guid>(
                name: "ParamId",
                table: "Questions",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "RelatedQuestionId",
                table: "Questions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParamValues_ParamId",
                table: "ParamValues",
                column: "ParamId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParamValues_Params_ParamId",
                table: "ParamValues",
                column: "ParamId",
                principalTable: "Params",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Params_ParamId",
                table: "Questions",
                column: "ParamId",
                principalTable: "Params",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParamValues_Params_ParamId",
                table: "ParamValues");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Params_ParamId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_ParamValues_ParamId",
                table: "ParamValues");

            migrationBuilder.DropColumn(
                name: "RelatedQuestionId",
                table: "Questions");

            migrationBuilder.AlterColumn<Guid>(
                name: "ParamId",
                table: "Questions",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Params_ParamId",
                table: "Questions",
                column: "ParamId",
                principalTable: "Params",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
