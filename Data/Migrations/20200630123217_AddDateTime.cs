using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParamId",
                table: "ReceptParams");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateTimeCreate",
                table: "ReceptRows",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "ReceptRows",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateTimeCreate",
                table: "ReceptParams",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ParamValueId",
                table: "ReceptParams",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateTimeCreate",
                table: "ParamValues",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTimeCreate",
                table: "ReceptRows");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "ReceptRows");

            migrationBuilder.DropColumn(
                name: "DateTimeCreate",
                table: "ReceptParams");

            migrationBuilder.DropColumn(
                name: "ParamValueId",
                table: "ReceptParams");

            migrationBuilder.DropColumn(
                name: "DateTimeCreate",
                table: "ParamValues");

            migrationBuilder.AddColumn<Guid>(
                name: "ParamId",
                table: "ReceptParams",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
