using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WpfPerfBench.Data.Migrations.SqlServer
{
    /// <inheritdoc />
    public partial class AddItemEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryId = table.Column<int>(type: "int", nullable: false, comment: "Привязка к категории"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "Название элемента"),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Статус"),
                    Priority = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Приоритет"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата создания"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Цена"),
                    Weight = table.Column<float>(type: "real", nullable: false, comment: "Вес"),
                    IsFragile = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Хрупкий"),
                    IsUrgent = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Срочный"),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Комментарий"),
                    Latitude = table.Column<double>(type: "float", nullable: false, comment: "Широта (отправление)"),
                    Longitude = table.Column<double>(type: "float", nullable: false, comment: "Долгота (отправление)"),
                    DeliveryLatitude = table.Column<double>(type: "float", nullable: false, comment: "Широта (доставки)"),
                    DeliveryLongitude = table.Column<double>(type: "float", nullable: false, comment: "Долгота (доставки)"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Удалено")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Item_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Item_CategoryId",
                table: "Item",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Item");
        }
    }
}