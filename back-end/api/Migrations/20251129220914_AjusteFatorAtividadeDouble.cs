using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeaceApi.Migrations
{
    /// <inheritdoc />
    public partial class AjusteFatorAtividadeDouble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FatorAtividade",
                table: "AvaliacoesAntropometricas",
                type: "character varying(64)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FatorAtividade",
                table: "AvaliacoesAntropometricas",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(64)");
        }
    }
}
