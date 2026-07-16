using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WpfPerfBench.Data.Migrations.Postgres
{
    /// <inheritdoc />
    public partial class HasData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Color", "IsActive", "Name", "ParentId" },
                values: new object[,]
                {
                    { 1, "#000000", true, "Все заказы", null },
                    { 2, "#333333", true, "По статусам", 1 },
                    { 7, "#333333", true, "По зонам", 1 },
                    { 3, "#4CAF50", true, "Новые", 2 },
                    { 4, "#FF9800", true, "В пути", 2 },
                    { 5, "#2196F3", true, "Доставлены", 2 },
                    { 6, "#F44336", true, "Отменены", 2 },
                    { 8, "#E91E63", true, "Центр", 7 },
                    { 9, "#00BCD4", true, "Север", 7 },
                    { 10, "#88C34A", true, "Юг", 7 },
                    { 11, "#9C27B0", true, "Запад", 7 },
                    { 12, "#FF5722", true, "Восток", 7 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
