using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infarstuructre.Migrations
{
    /// <inheritdoc />
    public partial class TBProjectInformations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBProjectInformations",
                columns: table => new
                {
                    IdProjectInformation = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProjectType = table.Column<int>(type: "int", nullable: false),
                    ProjectName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ProjectDescription = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
                    ProjectNameAr = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ProjectDescriptionAr = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
                    ProjectStart = table.Column<DateOnly>(type: "date", nullable: false),
                    ProjectEnd = table.Column<DateOnly>(type: "date", nullable: false),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBProjectInformations", x => x.IdProjectInformation);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBProjectInformations");
        }
    }
}
