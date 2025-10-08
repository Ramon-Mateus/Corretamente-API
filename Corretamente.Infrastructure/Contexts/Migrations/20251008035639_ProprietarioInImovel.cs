using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Corretamente.Infrastructure.Contexts.Migrations
{
    /// <inheritdoc />
    public partial class ProprietarioInImovel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Imoveis");

            migrationBuilder.AddColumn<int>(
                name: "ProprietarioId",
                table: "Imoveis",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Imoveis_ProprietarioId",
                table: "Imoveis",
                column: "ProprietarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Imoveis_Clientes_ProprietarioId",
                table: "Imoveis",
                column: "ProprietarioId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Imoveis_Clientes_ProprietarioId",
                table: "Imoveis");

            migrationBuilder.DropIndex(
                name: "IX_Imoveis_ProprietarioId",
                table: "Imoveis");

            migrationBuilder.DropColumn(
                name: "ProprietarioId",
                table: "Imoveis");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Imoveis",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
