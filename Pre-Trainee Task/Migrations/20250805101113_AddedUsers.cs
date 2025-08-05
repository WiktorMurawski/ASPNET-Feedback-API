using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Pre_Trainee_Task.Migrations
{
    /// <inheritdoc />
    public partial class AddedUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Feedbacks",
                columns: new[] { "Id", "CreatedAt", "Message", "Status", "Title", "Type", "UserId" },
                values: new object[,]
                {
                    { new Guid("5c3a8ae3-26cd-46a4-be27-b292891c5557"), new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Local), "Suggestion description message", 1, "Suggestion Title", 1, new Guid("f0a01a8c-67fe-4e97-9891-14534d75f185") },
                    { new Guid("be8743a9-b2b0-4168-9877-987dab1c2f25"), new DateTime(2025, 8, 5, 10, 11, 12, 721, DateTimeKind.Utc).AddTicks(6132), "Bug description message", 0, "Bug Title", 0, new Guid("87c013e7-726a-4a7c-ab6e-67b04ab54923") },
                    { new Guid("ffb38de6-255f-486f-9f4f-8c6a6080279d"), new DateTime(2025, 7, 29, 10, 11, 12, 721, DateTimeKind.Utc).AddTicks(6648), "Question message", 2, "Question Title", 2, new Guid("56a3ae7d-2aa4-45ca-a5ad-c7b8f4723d82") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DeleteData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: new Guid("5c3a8ae3-26cd-46a4-be27-b292891c5557"));

            migrationBuilder.DeleteData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: new Guid("be8743a9-b2b0-4168-9877-987dab1c2f25"));

            migrationBuilder.DeleteData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: new Guid("ffb38de6-255f-486f-9f4f-8c6a6080279d"));

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
    }
}
