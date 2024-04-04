using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KarlShoes.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class OrderProductEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Products_ProductID",
                table: "OrderProducts");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "OrderProducts",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProducts_ProductID",
                table: "OrderProducts",
                newName: "IX_OrderProducts_ProductId");

            migrationBuilder.AddColumn<string>(
                name: "OrderPDfUrl",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "OrderProducts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "OrderProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProductCode",
                table: "OrderProducts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "OrderProducts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "OrderProducts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Products_ProductId",
                table: "OrderProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Products_ProductId",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "OrderPDfUrl",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "ProductCode",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "OrderProducts");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "OrderProducts",
                newName: "ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProducts_ProductId",
                table: "OrderProducts",
                newName: "IX_OrderProducts_ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Products_ProductID",
                table: "OrderProducts",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
