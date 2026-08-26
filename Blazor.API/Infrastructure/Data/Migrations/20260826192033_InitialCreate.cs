using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Blazor.API.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuantidadeEstoque = table.Column<int>(type: "INTEGER", nullable: false),
                    Categoria = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "Categoria", "DataCriacao", "Nome", "Preco", "QuantidadeEstoque" },
                values: new object[,]
                {
                    { new Guid("0832ba9b-8ff1-46de-9f1e-ee33776af904"), "Categoria A", new DateTime(2026, 8, 26, 19, 20, 32, 738, DateTimeKind.Utc).AddTicks(1164), "Produto 1", 10.99m, 100 },
                    { new Guid("b35f8970-f1a0-4aaf-9ebe-ce754cb7aedd"), "Categoria A", new DateTime(2026, 8, 26, 19, 20, 32, 738, DateTimeKind.Utc).AddTicks(1192), "Produto 3", 15.75m, 75 },
                    { new Guid("e57736c7-67ea-47ac-8127-3d3257b9c089"), "Categoria B", new DateTime(2026, 8, 26, 19, 20, 32, 738, DateTimeKind.Utc).AddTicks(1190), "Produto 2", 20.50m, 50 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produtos");
        }
    }
}
