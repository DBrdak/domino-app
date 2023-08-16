using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Order.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OnlineOrderEntityUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Orders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "OrderItems",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalValue",
                table: "OrderItems",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "TotalValue",
                table: "OrderItems");
        }
    }
}