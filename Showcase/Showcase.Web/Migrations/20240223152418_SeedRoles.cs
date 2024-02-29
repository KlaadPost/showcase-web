using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Showcase.Web.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "00e6ac92-27f7-47af-a558-a2113891835e", null, "Administrator", "ADMINISTRATOR" },
                    { "89afe58c-fdc4-4665-a83f-8799713ded63", null, "None", "NONE" },
                    { "e970d790-099b-4c64-9e7e-19c7bde0b3aa", null, "Moderator", "MODERATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "00e6ac92-27f7-47af-a558-a2113891835e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89afe58c-fdc4-4665-a83f-8799713ded63");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e970d790-099b-4c64-9e7e-19c7bde0b3aa");
        }
    }
}
