using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class StartMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MedcinParams",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MedcinId = table.Column<Guid>(nullable: false),
                    ParamsValueId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedcinParams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medcins",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medcins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Params",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    HasUnit = table.Column<bool>(nullable: false),
                    Type = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Params", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParamValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    QuestionId = table.Column<Guid>(nullable: false),
                    ParamId = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    Unit = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParamValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionTrees",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionTrees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReceptParams",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ReceptId = table.Column<Guid>(nullable: false),
                    ParamId = table.Column<Guid>(nullable: false),
                    ReceptRowId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceptParams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReceptRows",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ReceptId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceptRows", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recepts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Template = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recepts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Scenarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    QuestionTreeId = table.Column<Guid>(nullable: false),
                    StartDate = table.Column<DateTimeOffset>(nullable: false),
                    Finished = table.Column<bool>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scenarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    QuestionTreeId = table.Column<Guid>(nullable: false),
                    ParametrId = table.Column<Guid>(nullable: false),
                    ParamId = table.Column<Guid>(nullable: true),
                    IsFirst = table.Column<bool>(nullable: false),
                    IsLast = table.Column<bool>(nullable: false),
                    ScenarioId = table.Column<Guid>(nullable: false),
                    ScenarioType = table.Column<int>(nullable: false),
                    ResponseType = table.Column<int>(nullable: false),
                    Scenario = table.Column<string>(nullable: true),
                    Answers = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Params_ParamId",
                        column: x => x.ParamId,
                        principalTable: "Params",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionTreeHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    QuestionId = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTimeOffset>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    ScenarioId = table.Column<Guid>(nullable: false),
                    MedcinId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionTreeHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionTreeHistories_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_ParamId",
                table: "Questions",
                column: "ParamId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTreeHistories_QuestionId",
                table: "QuestionTreeHistories",
                column: "QuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedcinParams");

            migrationBuilder.DropTable(
                name: "Medcins");

            migrationBuilder.DropTable(
                name: "ParamValues");

            migrationBuilder.DropTable(
                name: "QuestionTreeHistories");

            migrationBuilder.DropTable(
                name: "QuestionTrees");

            migrationBuilder.DropTable(
                name: "ReceptParams");

            migrationBuilder.DropTable(
                name: "ReceptRows");

            migrationBuilder.DropTable(
                name: "Recepts");

            migrationBuilder.DropTable(
                name: "Scenarios");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Params");
        }
    }
}
