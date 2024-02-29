using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Showcase.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddMutedToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Muted",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Muted",
                table: "AspNetUsers");
        }
    }
}
