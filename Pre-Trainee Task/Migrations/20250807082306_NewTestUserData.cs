using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Pre_Trainee_Task.Migrations
{
    /// <inheritdoc />
    public partial class NewTestUserData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: new Guid("20510160-5b13-4a0a-b66e-2a48e999ccaa"));

            migrationBuilder.DeleteData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: new Guid("3fe40238-32a5-4b69-9cad-21fd725b9081"));

            migrationBuilder.DeleteData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: new Guid("4c007bbf-5a54-4400-8400-6888eee3a96d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0f1f0773-19f1-44f3-acb0-6cab47c21421"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("559f47a5-7a44-4d9a-bfec-423a26b7ab50"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Feedbacks",
                columns: new[] { "Id", "CreatedAt", "Message", "Status", "Title", "Type", "UserId" },
                values: new object[,]
                {
                    { new Guid("20510160-5b13-4a0a-b66e-2a48e999ccaa"), new DateTime(2025, 8, 6, 13, 24, 45, 695, DateTimeKind.Utc).AddTicks(9419), "Bug description message", 0, "Bug Title", 0, new Guid("dbfdb578-3cbc-4b76-b02e-29fd4737535b") },
                    { new Guid("3fe40238-32a5-4b69-9cad-21fd725b9081"), new DateTime(2025, 8, 5, 0, 0, 0, 0, DateTimeKind.Local), "Suggestion description message", 1, "Suggestion Title", 1, new Guid("1ab468bb-57c1-4ee2-9625-5c2e1a042b29") },
                    { new Guid("4c007bbf-5a54-4400-8400-6888eee3a96d"), new DateTime(2025, 7, 30, 13, 24, 45, 695, DateTimeKind.Utc).AddTicks(9618), "Question message", 2, "Question Title", 2, new Guid("62b2e601-bc6f-47f5-bf33-03383c7513b8") }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "Role" },
                values: new object[,]
                {
                    { new Guid("0f1f0773-19f1-44f3-acb0-6cab47c21421"), "user@user.com", "$2a$11$g1JUp8OQdddHlm/irG1DP.JwwyulCrjIIaBTmmImw4WFYeDK9eOym", 0 },
                    { new Guid("559f47a5-7a44-4d9a-bfec-423a26b7ab50"), "admin@admin.com", "$2a$11$3fHCSriGctlLwgg78CuG5eeFT9rUwVaz8uT.aCG8k1paU1TNAz2PS", 1 }
                });
        }
    }
}
