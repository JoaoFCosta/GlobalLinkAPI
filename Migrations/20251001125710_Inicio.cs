using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlobalLinkAPI.Migrations
{
    /// <inheritdoc />
    public partial class Inicio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    EmpresaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaNome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpresaCnpj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpresaEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpresaTelefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpresaSenha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpresaRua = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpresaNumero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpresaBairro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpresaCep = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpresaComplemento = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.EmpresaId);
                });

            migrationBuilder.CreateTable(
                name: "Ongs",
                columns: table => new
                {
                    OngId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OngNome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OngCnpj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OngEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OngTelefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OngSenha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OngRua = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OngNumero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OngBairro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OngCep = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OngComplemento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OngMissao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ongs", x => x.OngId);
                });

            migrationBuilder.CreateTable(
                name: "Needs",
                columns: table => new
                {
                    NecessidadeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NecessidadeTitulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NecessidadeDescricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NecessidadeCategoria = table.Column<int>(type: "int", nullable: false),
                    NecessidadeUrgencia = table.Column<int>(type: "int", nullable: false),
                    Local = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OngId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Needs", x => x.NecessidadeId);
                    table.ForeignKey(
                        name: "FK_Needs_Ongs_OngId",
                        column: x => x.OngId,
                        principalTable: "Ongs",
                        principalColumn: "OngId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Donations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaId = table.Column<int>(type: "int", nullable: false),
                    CompanyEmpresaId = table.Column<int>(type: "int", nullable: true),
                    OngId = table.Column<int>(type: "int", nullable: false),
                    NecessidadeId = table.Column<int>(type: "int", nullable: true),
                    NeedNecessidadeId = table.Column<int>(type: "int", nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantidade = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Observacoes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataRecebimento = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Donations_Companies_CompanyEmpresaId",
                        column: x => x.CompanyEmpresaId,
                        principalTable: "Companies",
                        principalColumn: "EmpresaId");
                    table.ForeignKey(
                        name: "FK_Donations_Needs_NeedNecessidadeId",
                        column: x => x.NeedNecessidadeId,
                        principalTable: "Needs",
                        principalColumn: "NecessidadeId");
                    table.ForeignKey(
                        name: "FK_Donations_Ongs_OngId",
                        column: x => x.OngId,
                        principalTable: "Ongs",
                        principalColumn: "OngId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Donations_CompanyEmpresaId",
                table: "Donations",
                column: "CompanyEmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Donations_NeedNecessidadeId",
                table: "Donations",
                column: "NeedNecessidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Donations_OngId",
                table: "Donations",
                column: "OngId");

            migrationBuilder.CreateIndex(
                name: "IX_Needs_OngId",
                table: "Needs",
                column: "OngId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Donations");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Needs");

            migrationBuilder.DropTable(
                name: "Ongs");
        }
    }
}
