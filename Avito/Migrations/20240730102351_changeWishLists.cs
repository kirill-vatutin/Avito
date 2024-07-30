using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Avito.Migrations
{
    /// <inheritdoc />
    public partial class changeWishLists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_WishLists_UserId",
                table: "WishLists",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WishLists_Users_UserId",
                table: "WishLists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishLists_Users_UserId",
                table: "WishLists");

            migrationBuilder.DropIndex(
                name: "IX_WishLists_UserId",
                table: "WishLists");
        }
    }
}
