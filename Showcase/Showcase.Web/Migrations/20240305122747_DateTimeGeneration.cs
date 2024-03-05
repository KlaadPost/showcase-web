using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Showcase.Web.Migrations
{
    /// <inheritdoc />
    public partial class DateTimeGeneration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8262b86b-8f63-4406-8dbc-73d7abc4a315");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad852bbf-8595-42ad-af34-46dcdd6416d4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c03fc6f8-7452-421b-90c3-777e491bbbc9");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "ChatMessages",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "ChatMessages",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2f626240-abc4-4207-a368-1f88e830abd7", null, "Administrator", "ADMINISTRATOR" },
                    { "8d63448b-6b88-4d83-945c-94cbb85220a6", null, "None", "NONE" },
                    { "fa4766f3-ceda-4d18-a1dc-a17bb935db4f", null, "Moderator", "MODERATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f626240-abc4-4207-a368-1f88e830abd7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8d63448b-6b88-4d83-945c-94cbb85220a6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa4766f3-ceda-4d18-a1dc-a17bb935db4f");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "ChatMessages",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "ChatMessages",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8262b86b-8f63-4406-8dbc-73d7abc4a315", null, "Moderator", "MODERATOR" },
                    { "ad852bbf-8595-42ad-af34-46dcdd6416d4", null, "None", "NONE" },
                    { "c03fc6f8-7452-421b-90c3-777e491bbbc9", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
