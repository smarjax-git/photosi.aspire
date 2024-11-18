using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoSi.Prodotti.Migrations
{
    /// <inheritdoc />
    public partial class Prodotto_Added_Active : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Active",
                schema: "dbo",
                table: "Prodotti",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                schema: "dbo",
                table: "Prodotti");
        }
    }
}
