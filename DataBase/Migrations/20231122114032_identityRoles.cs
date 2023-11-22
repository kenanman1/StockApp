using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataBase.Migrations
{
    /// <inheritdoc />
    public partial class identityRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1f0d5f32-e97a-4c32-a3fb-9d86b3ccec57", "2217ac14-c947-4267-9bf3-ce82f49b8673", "User", "USER" },
                    { "6d579265-c44f-45a7-9a18-ef4aa8a1baa0", "70f4f0ac-7d06-4153-8fdc-cacbf26bd3d4", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1f0d5f32-e97a-4c32-a3fb-9d86b3ccec57");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6d579265-c44f-45a7-9a18-ef4aa8a1baa0");
        }
    }
}
