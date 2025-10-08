using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlobalLinkAPI.Migrations
{
    /// <inheritdoc />
    public partial class Donate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NecessidadeCategoria",
                table: "Needs");

            migrationBuilder.DropColumn(
                name: "NecessidadeUrgencia",
                table: "Needs");

            migrationBuilder.AddColumn<string>(
                name: "Categoria",
                table: "Needs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Urgencia",
                table: "Needs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Donations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Categoria",
                table: "Needs");

            migrationBuilder.DropColumn(
                name: "Urgencia",
                table: "Needs");

            migrationBuilder.AddColumn<int>(
                name: "NecessidadeCategoria",
                table: "Needs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NecessidadeUrgencia",
                table: "Needs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Donations",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
