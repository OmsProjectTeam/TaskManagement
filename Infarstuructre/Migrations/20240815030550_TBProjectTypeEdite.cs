using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infarstuructre.Migrations
{
    /// <inheritdoc />
    public partial class TBProjectTypeEdite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProjectTypesAr",
                table: "TBProjectTypes",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectTypesAr",
                table: "TBProjectTypes");
        }
    }
}
