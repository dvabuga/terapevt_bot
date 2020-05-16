using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class FixMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Params_ParamId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "ParametrId",
                table: "Questions");

            migrationBuilder.AlterColumn<Guid>(
                name: "ParamId",
                table: "Questions",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Params_ParamId",
                table: "Questions",
                column: "ParamId",
                principalTable: "Params",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Params_ParamId",
                table: "Questions");

            migrationBuilder.AlterColumn<Guid>(
                name: "ParamId",
                table: "Questions",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "ParametrId",
                table: "Questions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Params_ParamId",
                table: "Questions",
                column: "ParamId",
                principalTable: "Params",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
