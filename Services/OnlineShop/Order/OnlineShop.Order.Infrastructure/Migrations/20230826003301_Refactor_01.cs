using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Order.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Refactor_01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCanceled",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsRejected",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "TotalValue",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "Currency",
                table: "Orders",
                newName: "TotalPrice_Currency");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Orders",
                newName: "TotalPrice_Unit");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Orders",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Unit",
                table: "OrderItems",
                newName: "TotalValue_Unit");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "OrderItems",
                newName: "TotalValue_Currency");

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletionDate",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status_StatusMessage",
                table: "Orders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice_Amount",
                table: "Orders",
                type: "numeric",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "OrderItems",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "Quantity_Unit",
                table: "OrderItems",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Quantity_Value",
                table: "OrderItems",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalValue_Amount",
                table: "OrderItems",
                type: "numeric",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletionDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Status_StatusMessage",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TotalPrice_Amount",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Quantity_Unit",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Quantity_Value",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "TotalValue_Amount",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "TotalPrice_Currency",
                table: "Orders",
                newName: "Currency");

            migrationBuilder.RenameColumn(
                name: "TotalPrice_Unit",
                table: "Orders",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Orders",
                newName: "OrderId");

            migrationBuilder.RenameColumn(
                name: "TotalValue_Unit",
                table: "OrderItems",
                newName: "Unit");

            migrationBuilder.RenameColumn(
                name: "TotalValue_Currency",
                table: "OrderItems",
                newName: "Status");

            migrationBuilder.AddColumn<bool>(
                name: "IsCanceled",
                table: "Orders",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmed",
                table: "Orders",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRejected",
                table: "Orders",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Orders",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "OrderItems",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<decimal>(
                name: "Quantity",
                table: "OrderItems",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalValue",
                table: "OrderItems",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}