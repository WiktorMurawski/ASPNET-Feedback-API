using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Pre_Trainee_Task.Migrations
{
    /// <inheritdoc />
    public partial class AuditLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: new Guid("14ea5e54-ac6a-48ce-89c8-bffe04843b40"));

            migrationBuilder.DeleteData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: new Guid("17b19f7b-85d7-4f0a-8df0-7cefbde94e2d"));

            migrationBuilder.DeleteData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: new Guid("4fc0f104-9587-48ee-ac9e-0521458f59dd"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bfbc0d4a-83ec-4d10-9170-e2dad71bfa2a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("fc521106-c115-4152-bcf2-7a7044e90e94"));

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FeedbackId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Method = table.Column<int>(type: "INTEGER", nullable: false),
                    Actor = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

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
                    { new Guid("14ea5e54-ac6a-48ce-89c8-bffe04843b40"), new DateTime(2025, 8, 6, 9, 53, 51, 421, DateTimeKind.Utc).AddTicks(5123), "Bug description message", 0, "Bug Title", 0, new Guid("6a1d338f-8a9a-43c7-bb79-6fbe9f6d82d9") },
                    { new Guid("17b19f7b-85d7-4f0a-8df0-7cefbde94e2d"), new DateTime(2025, 7, 30, 9, 53, 51, 421, DateTimeKind.Utc).AddTicks(5438), "Question message", 2, "Question Title", 2, new Guid("5c166b8f-e264-427c-8eff-d68664a6eacc") },
                    { new Guid("4fc0f104-9587-48ee-ac9e-0521458f59dd"), new DateTime(2025, 8, 5, 0, 0, 0, 0, DateTimeKind.Local), "Suggestion description message", 1, "Suggestion Title", 1, new Guid("947ee152-6f9b-481f-87cf-50fcc89e9072") }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "Role" },
                values: new object[,]
                {
                    { new Guid("bfbc0d4a-83ec-4d10-9170-e2dad71bfa2a"), "admin@admin.com", "$2a$11$T/pSyXQjLDIHE2ts56gAlOnD3iYpHgBwHid5tiiHAA2EuBkN02Urq", 1 },
                    { new Guid("fc521106-c115-4152-bcf2-7a7044e90e94"), "user@user.com", "$2a$11$gFry0rbXS.HytKr7SGpbcuOQh3rWQ2/LMLHmnIUFZbgC8ltWcmynm", 0 }
                });
        }
    }
}
