using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Pre_Trainee_Task.Migrations
{
    /// <inheritdoc />
    public partial class ModelAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: new Guid("25358b8d-d761-4b16-93ca-2786c9438ac5"));

            migrationBuilder.DeleteData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: new Guid("99ef5e49-a788-4d32-baf7-831547582a00"));

            migrationBuilder.DeleteData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: new Guid("f30c7cea-7ce9-4ea5-8d07-1183af05332c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0252de01-d95a-470b-b00a-17ffbb5bce78"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e63979ec-e7cc-4be6-a6e6-201008979a29"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Feedbacks",
                columns: new[] { "Id", "CreatedAt", "Message", "Status", "Title", "Type", "UserId" },
                values: new object[,]
                {
                    { new Guid("25358b8d-d761-4b16-93ca-2786c9438ac5"), new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Local), "Suggestion description message", 1, "Suggestion Title", 1, new Guid("e8ceaad0-68aa-44c4-a037-6ad88e5d5483") },
                    { new Guid("99ef5e49-a788-4d32-baf7-831547582a00"), new DateTime(2025, 8, 5, 12, 25, 37, 294, DateTimeKind.Utc).AddTicks(707), "Bug description message", 0, "Bug Title", 0, new Guid("3d3f62d1-2fbf-487a-b46d-5f74b5162c66") },
                    { new Guid("f30c7cea-7ce9-4ea5-8d07-1183af05332c"), new DateTime(2025, 7, 29, 12, 25, 37, 294, DateTimeKind.Utc).AddTicks(1032), "Question message", 2, "Question Title", 2, new Guid("13d7cc02-3b5d-4a3e-b711-9e226479fa20") }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "Role" },
                values: new object[,]
                {
                    { new Guid("0252de01-d95a-470b-b00a-17ffbb5bce78"), "user@user.com", "$2a$11$VpPm1lXoGx7Y6O0qoF97D.Kw/xFPZ5V79n9mKPTcip3QM/a9.4TTO", 0 },
                    { new Guid("e63979ec-e7cc-4be6-a6e6-201008979a29"), "admin@admin.com", "$2a$11$XaZwPNX.YZ300ZXQuVDp6.vvPJXRhaxWdmv1HM1C/.bugtEJe45Om", 1 }
                });
        }
    }
}
