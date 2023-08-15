using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Order.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OnlineOrderEntityUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "OrderItems",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unit",
                table: "OrderItems");
        }
    }
}
