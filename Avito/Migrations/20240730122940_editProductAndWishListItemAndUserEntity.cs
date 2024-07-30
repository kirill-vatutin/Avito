using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Avito.Migrations
{
    /// <inheritdoc />
    public partial class editProductAndWishListItemAndUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "WishLists",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TelegramChatId",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_WishLists_ProductId",
                table: "WishLists",
                column: "ProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WishLists_Products_ProductId",
                table: "WishLists",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishLists_Products_ProductId",
                table: "WishLists");

            migrationBuilder.DropIndex(
                name: "IX_WishLists_ProductId",
                table: "WishLists");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "WishLists");

            migrationBuilder.DropColumn(
                name: "TelegramChatId",
                table: "Users");
        }
    }
}
