using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MuseumApplication.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedInDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VisitorHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitorHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VisitorHistories_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VisitorInHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitorHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitorInHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VisitorInHistories_VisitorHistories_VisitorHistoryId",
                        column: x => x.VisitorHistoryId,
                        principalTable: "VisitorHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VisitorInHistories_Visitors_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "Visitors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VisitorHistories_OwnerId",
                table: "VisitorHistories",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitorInHistories_VisitorHistoryId",
                table: "VisitorInHistories",
                column: "VisitorHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitorInHistories_VisitorId",
                table: "VisitorInHistories",
                column: "VisitorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VisitorInHistories");

            migrationBuilder.DropTable(
                name: "VisitorHistories");
        }
    }
}
