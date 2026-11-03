using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechRepair.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    NIF = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Telefone = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Morada = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    CodigoPostal = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Localidade = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    DataRegisto = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tecnicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Telefone = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Especialidade = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tecnicos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Equipamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClienteId = table.Column<int>(type: "INTEGER", nullable: false),
                    TipoEquipamento = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Marca = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Modelo = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    NumeroSerie = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    DataRegisto = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipamentos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reparacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EquipamentoId = table.Column<int>(type: "INTEGER", nullable: false),
                    TecnicoId = table.Column<int>(type: "INTEGER", nullable: true),
                    DataEntrada = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DescricaoAvaria = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    Diagnostico = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    Estado = table.Column<int>(type: "INTEGER", nullable: false),
                    DataConclusao = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DataEntrega = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Observacoes = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reparacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reparacoes_Equipamentos_EquipamentoId",
                        column: x => x.EquipamentoId,
                        principalTable: "Equipamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reparacoes_Tecnicos_TecnicoId",
                        column: x => x.TecnicoId,
                        principalTable: "Tecnicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Intervencoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReparacaoId = table.Column<int>(type: "INTEGER", nullable: false),
                    DataIntervencao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DescricaoTrabalho = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    PecasUtilizadas = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    TempoTrabalho = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CustoMaoObra = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CustoPecas = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Observacoes = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intervencoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Intervencoes_Reparacoes_ReparacaoId",
                        column: x => x.ReparacaoId,
                        principalTable: "Reparacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_Email",
                table: "Clientes",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_NIF",
                table: "Clientes",
                column: "NIF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Equipamentos_ClienteId",
                table: "Equipamentos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipamentos_NumeroSerie",
                table: "Equipamentos",
                column: "NumeroSerie",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Intervencoes_ReparacaoId",
                table: "Intervencoes",
                column: "ReparacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Reparacoes_EquipamentoId",
                table: "Reparacoes",
                column: "EquipamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Reparacoes_TecnicoId",
                table: "Reparacoes",
                column: "TecnicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tecnicos_Email",
                table: "Tecnicos",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Intervencoes");

            migrationBuilder.DropTable(
                name: "Reparacoes");

            migrationBuilder.DropTable(
                name: "Equipamentos");

            migrationBuilder.DropTable(
                name: "Tecnicos");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
