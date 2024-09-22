using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infarstuructre.Migrations
{
    /// <inheritdoc />
    public partial class TBRequestsTaskEdite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "TBRequestsTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "TBRequestsTasks");
        }
    }
}
