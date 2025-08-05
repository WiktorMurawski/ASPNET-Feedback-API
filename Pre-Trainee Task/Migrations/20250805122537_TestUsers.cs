using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Pre_Trainee_Task.Migrations
{
    /// <inheritdoc />
    public partial class TestUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { new Guid("5c3a8ae3-26cd-46a4-be27-b292891c5557"), new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Local), "Suggestion description message", 1, "Suggestion Title", 1, new Guid("f0a01a8c-67fe-4e97-9891-14534d75f185") },
                    { new Guid("be8743a9-b2b0-4168-9877-987dab1c2f25"), new DateTime(2025, 8, 5, 10, 11, 12, 721, DateTimeKind.Utc).AddTicks(6132), "Bug description message", 0, "Bug Title", 0, new Guid("87c013e7-726a-4a7c-ab6e-67b04ab54923") },
                    { new Guid("ffb38de6-255f-486f-9f4f-8c6a6080279d"), new DateTime(2025, 7, 29, 10, 11, 12, 721, DateTimeKind.Utc).AddTicks(6648), "Question message", 2, "Question Title", 2, new Guid("56a3ae7d-2aa4-45ca-a5ad-c7b8f4723d82") }
                });
        }
    }
}
