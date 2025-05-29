using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobsApplication.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserToApplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Applications",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_OwnerId",
                table: "Applications",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_AspNetUsers_OwnerId",
                table: "Applications",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_AspNetUsers_OwnerId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_OwnerId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Applications");
        }
    }
}
