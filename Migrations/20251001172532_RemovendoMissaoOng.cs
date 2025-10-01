using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlobalLinkAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemovendoMissaoOng : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OngMissao",
                table: "Ongs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OngMissao",
                table: "Ongs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
