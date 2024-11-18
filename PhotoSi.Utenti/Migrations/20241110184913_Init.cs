using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoSi.Utenti.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreferredPickupPoints",
                schema: "dbo",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "UserPickupPoints",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PickupPointId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPickupPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPickupPoints_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPickupPoints_UserId",
                schema: "dbo",
                table: "UserPickupPoints",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPickupPoints",
                schema: "dbo");

            migrationBuilder.AddColumn<string>(
                name: "PreferredPickupPoints",
                schema: "dbo",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
