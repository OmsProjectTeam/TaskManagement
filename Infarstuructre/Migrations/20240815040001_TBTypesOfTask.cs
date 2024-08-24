using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infarstuructre.Migrations
{
    /// <inheritdoc />
    public partial class TBTypesOfTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBTypesOfTasks",
                columns: table => new
                {
                    IdTypesOfTask = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypesOfTask = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TypesOfTaskAr = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBTypesOfTasks", x => x.IdTypesOfTask);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBTypesOfTasks");
        }
    }
}
