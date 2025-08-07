using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Pre_Trainee_Task.Migrations
{
    /// <inheritdoc />
    public partial class AuditLogsChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: new Guid("51796d6f-12b8-4e3a-a52a-8ed73bfeb2a3"));

            migrationBuilder.DeleteData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: new Guid("771fd9e5-12b3-45ab-9ddb-185eab01516c"));

            migrationBuilder.DeleteData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: new Guid("fe6aa8d8-8b0b-40a3-9e0a-6b6710132948"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0a8e1a43-1a2b-4ae7-bfd9-058a81cb6ceb"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b0c45641-a6bd-4d18-b575-eb258c2e15c4"));

            migrationBuilder.RenameColumn(
                name: "Actor",
                table: "AuditLogs",
                newName: "Email");

            migrationBuilder.InsertData(
                table: "Feedbacks",
                columns: new[] { "Id", "CreatedAt", "Message", "Status", "Title", "Type", "UserId" },
                values: new object[,]
                {
                    { new Guid("1083acd1-7184-4f69-87f0-c6c2c8d356ca"), new DateTime(2025, 8, 6, 0, 0, 0, 0, DateTimeKind.Local), "Suggestion description message", 1, "Suggestion Title", 1, new Guid("55f928b5-2c59-433b-ad04-179e3bffca06") },
                    { new Guid("2902bd6b-7c51-4b73-9e3e-2ca82d6758a6"), new DateTime(2025, 7, 31, 12, 44, 58, 705, DateTimeKind.Utc).AddTicks(3647), "Question message", 2, "Question Title", 2, new Guid("511cf337-5cb7-425c-8993-ab03cb0c2d04") },
                    { new Guid("e0e12545-da81-4c90-9259-61fb9d5a0ae9"), new DateTime(2025, 8, 7, 12, 44, 58, 705, DateTimeKind.Utc).AddTicks(3441), "Bug description message", 0, "Bug Title", 0, new Guid("2d546791-bfc1-467a-8fab-48be1130fc91") }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "Role" },
                values: new object[,]
                {
                    { new Guid("2dfdf577-6749-4107-b576-50616e91bc1b"), "admin@admin.com", "$2a$11$VOPNgeLmzuxwMK01aCoT/uzpMhXgiew3jMA7brVurLDKvUSDk5iXe", 1 },
                    { new Guid("5a1edbc7-8fe6-4d7b-a560-aa49f858ac94"), "user@user.com", "$2a$11$khNXzAXpbJADyJkZttu43uzQEzyRu7QiCCIBYNBZ4aPXvZj/7cuIa", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: new Guid("1083acd1-7184-4f69-87f0-c6c2c8d356ca"));

            migrationBuilder.DeleteData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: new Guid("2902bd6b-7c51-4b73-9e3e-2ca82d6758a6"));

            migrationBuilder.DeleteData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: new Guid("e0e12545-da81-4c90-9259-61fb9d5a0ae9"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2dfdf577-6749-4107-b576-50616e91bc1b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5a1edbc7-8fe6-4d7b-a560-aa49f858ac94"));

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "AuditLogs",
                newName: "Actor");

            migrationBuilder.InsertData(
                table: "Feedbacks",
                columns: new[] { "Id", "CreatedAt", "Message", "Status", "Title", "Type", "UserId" },
                values: new object[,]
                {
                    { new Guid("51796d6f-12b8-4e3a-a52a-8ed73bfeb2a3"), new DateTime(2025, 8, 6, 0, 0, 0, 0, DateTimeKind.Local), "Suggestion description message", 1, "Suggestion Title", 1, new Guid("c2fe8f01-c50a-42ad-9ced-57a7a08e6a53") },
                    { new Guid("771fd9e5-12b3-45ab-9ddb-185eab01516c"), new DateTime(2025, 7, 31, 8, 23, 5, 516, DateTimeKind.Utc).AddTicks(978), "Question message", 2, "Question Title", 2, new Guid("7fb51546-f5e6-4a2d-ad3a-711f1ea14a48") },
                    { new Guid("fe6aa8d8-8b0b-40a3-9e0a-6b6710132948"), new DateTime(2025, 8, 7, 8, 23, 5, 516, DateTimeKind.Utc).AddTicks(653), "Bug description message", 0, "Bug Title", 0, new Guid("df747bf0-c59c-4d0e-ad03-57b3daef53d9") }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "Role" },
                values: new object[,]
                {
                    { new Guid("0a8e1a43-1a2b-4ae7-bfd9-058a81cb6ceb"), "user@user.com", "$2a$11$bffMJwOsVtn2kIEo0RROH.X6WbTdLbj5mdR6NxGbXm8q3kxN.oUty", 0 },
                    { new Guid("b0c45641-a6bd-4d18-b575-eb258c2e15c4"), "admin@admin.com", "$2a$11$9/.2Y102RKTiCvo91sI2n.4Riy/TTspYjcryfXGffaL.lH/fTSA/i", 1 }
                });
        }
    }
}
