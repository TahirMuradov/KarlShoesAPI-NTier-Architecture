using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KarlShoes.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SubCatgeoryAndProductAddedManytoMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubCategoriesProduct",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategoriesProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategoriesProduct_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubCategoriesProduct_subCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "subCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubCategoriesProduct_ProductId",
                table: "SubCategoriesProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategoriesProduct_SubCategoryId",
                table: "SubCategoriesProduct",
                column: "SubCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubCategoriesProduct");
        }
    }
}
