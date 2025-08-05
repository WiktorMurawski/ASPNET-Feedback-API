using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Pre_Trainee_Task.Migrations
{
    /// <inheritdoc />
    public partial class TestDataMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Feedbacks",
                columns: new[] { "Id", "CreatedAt", "Message", "Status", "Title", "Type", "UserId" },
                values: new object[,]
                {
                    { new Guid("0be68ac3-77fe-4a0e-ba2b-9d0a7f9cf998"), new DateTime(2025, 8, 5, 7, 26, 3, 559, DateTimeKind.Utc).AddTicks(993), "Bug description message", 0, "Bug Title", 0, new Guid("469d286f-0edb-4723-bf64-70db4a19d2c4") },
                    { new Guid("7fdfda00-6ceb-45ab-ad98-42105d839688"), new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Local), "Suggestion description message", 1, "Suggestion Title", 1, new Guid("1f691bb7-9228-4584-925d-49defd56e48c") },
                    { new Guid("807561cc-9a9f-4205-a850-70f8095a0837"), new DateTime(2025, 7, 29, 9, 26, 3, 559, DateTimeKind.Local).AddTicks(1217), "Question message", 2, "Question Title", 2, new Guid("1429e2d3-b0d2-4591-9468-91301173a6e7") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: new Guid("0be68ac3-77fe-4a0e-ba2b-9d0a7f9cf998"));

            migrationBuilder.DeleteData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: new Guid("7fdfda00-6ceb-45ab-ad98-42105d839688"));

            migrationBuilder.DeleteData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: new Guid("807561cc-9a9f-4205-a850-70f8095a0837"));
        }
    }
}
