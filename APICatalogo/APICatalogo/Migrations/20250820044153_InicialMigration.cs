using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace APICatalogo.Migrations
{
    /// <inheritdoc />
    public partial class InicialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TabelaCategorias",
                columns: table => new
                {
                    CategoriaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    ImagemUrl = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabelaCategorias", x => x.CategoriaId);
                });

            migrationBuilder.CreateTable(
                name: "TabelaProdutos",
                columns: table => new
                {
                    ProdutoId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Preco = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    ImagemUrl = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Estoque = table.Column<float>(type: "real", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CategoriaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabelaProdutos", x => x.ProdutoId);
                    table.ForeignKey(
                        name: "FK_TabelaProdutos_TabelaCategorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "TabelaCategorias",
                        principalColumn: "CategoriaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TabelaProdutos_CategoriaId",
                table: "TabelaProdutos",
                column: "CategoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TabelaProdutos");

            migrationBuilder.DropTable(
                name: "TabelaCategorias");
        }
    }
}
