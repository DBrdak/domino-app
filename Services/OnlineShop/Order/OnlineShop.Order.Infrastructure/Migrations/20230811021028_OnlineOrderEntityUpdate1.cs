using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Order.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OnlineOrderEntityUpdate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Unit",
                table: "OrderItems",
                newName: "Price_Unit");

            migrationBuilder.RenameColumn(
                name: "Currency",
                table: "OrderItems",
                newName: "Price_Currency");

            migrationBuilder.AddColumn<decimal>(
                name: "Price_Amount",
                table: "OrderItems",
                type: "numeric",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price_Amount",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "Price_Unit",
                table: "OrderItems",
                newName: "Unit");

            migrationBuilder.RenameColumn(
                name: "Price_Currency",
                table: "OrderItems",
                newName: "Currency");
        }
    }
}
