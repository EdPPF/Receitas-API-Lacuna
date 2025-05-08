using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_receitas.Migrations
{
    /// <inheritdoc />
    public partial class InitialPostgres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ingredientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: true),
                    Unidade = table.Column<string>(type: "text", nullable: true),
                    IsAtivo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingredientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "receitas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: true),
                    ModoPreparo = table.Column<string>(type: "text", nullable: true),
                    TempoPreparo = table.Column<int>(type: "integer", nullable: false),
                    Porcoes = table.Column<int>(type: "integer", nullable: false),
                    IsAtivo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_receitas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IngredienteModelReceitaModel",
                columns: table => new
                {
                    IngredientesId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReceitasId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredienteModelReceitaModel", x => new { x.IngredientesId, x.ReceitasId });
                    table.ForeignKey(
                        name: "FK_IngredienteModelReceitaModel_ingredientes_IngredientesId",
                        column: x => x.IngredientesId,
                        principalTable: "ingredientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredienteModelReceitaModel_receitas_ReceitasId",
                        column: x => x.ReceitasId,
                        principalTable: "receitas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "receita_ingrediente",
                columns: table => new
                {
                    ReceitaId = table.Column<Guid>(type: "uuid", nullable: false),
                    IngredienteId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantidade = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_receita_ingrediente", x => new { x.ReceitaId, x.IngredienteId });
                    table.ForeignKey(
                        name: "FK_receita_ingrediente_ingredientes_IngredienteId",
                        column: x => x.IngredienteId,
                        principalTable: "ingredientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_receita_ingrediente_receitas_ReceitaId",
                        column: x => x.ReceitaId,
                        principalTable: "receitas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredienteModelReceitaModel_ReceitasId",
                table: "IngredienteModelReceitaModel",
                column: "ReceitasId");

            migrationBuilder.CreateIndex(
                name: "IX_receita_ingrediente_IngredienteId",
                table: "receita_ingrediente",
                column: "IngredienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredienteModelReceitaModel");

            migrationBuilder.DropTable(
                name: "receita_ingrediente");

            migrationBuilder.DropTable(
                name: "ingredientes");

            migrationBuilder.DropTable(
                name: "receitas");
        }
    }
}
