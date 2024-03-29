using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KarlShoes.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SubCategoryLaunguageTableAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryLanguages_Categories_CategoryId",
                table: "CategoryLanguages");

            migrationBuilder.DropColumn(
                name: "LangCode",
                table: "subCategories");

            migrationBuilder.DropColumn(
                name: "SubcategoryName",
                table: "subCategories");

            migrationBuilder.CreateTable(
                name: "subCategoryLaunguages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubcategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LangCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subCategoryLaunguages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_subCategoryLaunguages_subCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "subCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_subCategoryLaunguages_SubCategoryId",
                table: "subCategoryLaunguages",
                column: "SubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryLanguages_Categories_CategoryId",
                table: "CategoryLanguages",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryLanguages_Categories_CategoryId",
                table: "CategoryLanguages");

            migrationBuilder.DropTable(
                name: "subCategoryLaunguages");

            migrationBuilder.AddColumn<string>(
                name: "LangCode",
                table: "subCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SubcategoryName",
                table: "subCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryLanguages_Categories_CategoryId",
                table: "CategoryLanguages",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
