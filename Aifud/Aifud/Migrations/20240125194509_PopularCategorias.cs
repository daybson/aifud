using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aifud.Migrations
{
    /// <inheritdoc />
    public partial class PopularCategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Categorias(Nome, Descricao) " +
                "VALUES ('Normal', 'Lanche feito com ingredientes sem restrições')");
            migrationBuilder.Sql("INSERT INTO Categorias(Nome, Descricao) " +
                    "VALUES ('Natural', 'Lanche feito com ingredientes integrais e naturais')");
            migrationBuilder.Sql("INSERT INTO Categorias(Nome, Descricao) " +
                "VALUES ('Vegano', 'Lanche feito com ingredientes sem derivados de carne')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELERE FROM Categorias");
        }
    }
}
