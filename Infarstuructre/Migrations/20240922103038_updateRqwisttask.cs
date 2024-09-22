using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infarstuructre.Migrations
{
    /// <inheritdoc />
    public partial class updateRqwisttask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RequestsTitelAr",
                table: "TBRequestsTasks",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RequestsTitelEn",
                table: "TBRequestsTasks",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestsTitelAr",
                table: "TBRequestsTasks");

            migrationBuilder.DropColumn(
                name: "RequestsTitelEn",
                table: "TBRequestsTasks");
        }
    }
}
