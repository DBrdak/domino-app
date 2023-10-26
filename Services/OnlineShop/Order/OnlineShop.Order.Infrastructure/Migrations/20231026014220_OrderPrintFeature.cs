using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Order.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OrderPrintFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPrinted",
                table: "Orders",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPrinted",
                table: "Orders");
        }
    }
}
