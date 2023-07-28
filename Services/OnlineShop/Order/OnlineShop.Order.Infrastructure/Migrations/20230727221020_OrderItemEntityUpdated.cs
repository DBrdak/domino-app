using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Order.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OrderItemEntityUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsCompleted",
                table: "Orders",
                newName: "IsConfirmed");

            migrationBuilder.RenameColumn(
                name: "IsConfirmed",
                table: "OrderItems",
                newName: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsConfirmed",
                table: "Orders",
                newName: "IsCompleted");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "OrderItems",
                newName: "IsConfirmed");
        }
    }
}
