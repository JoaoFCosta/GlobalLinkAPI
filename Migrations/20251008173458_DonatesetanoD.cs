using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlobalLinkAPI.Migrations
{
    /// <inheritdoc />
    public partial class DonatesetanoD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_Companies_CompanyEmpresaId",
                table: "Donations");

            migrationBuilder.DropIndex(
                name: "IX_Donations_CompanyEmpresaId",
                table: "Donations");

            migrationBuilder.DropColumn(
                name: "CompanyEmpresaId",
                table: "Donations");

            migrationBuilder.CreateIndex(
                name: "IX_Donations_EmpresaId",
                table: "Donations",
                column: "EmpresaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_Companies_EmpresaId",
                table: "Donations",
                column: "EmpresaId",
                principalTable: "Companies",
                principalColumn: "EmpresaId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_Companies_EmpresaId",
                table: "Donations");

            migrationBuilder.DropIndex(
                name: "IX_Donations_EmpresaId",
                table: "Donations");

            migrationBuilder.AddColumn<int>(
                name: "CompanyEmpresaId",
                table: "Donations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Donations_CompanyEmpresaId",
                table: "Donations",
                column: "CompanyEmpresaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_Companies_CompanyEmpresaId",
                table: "Donations",
                column: "CompanyEmpresaId",
                principalTable: "Companies",
                principalColumn: "EmpresaId");
        }
    }
}
