using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Usuario.Data.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AlterarConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
               name: "FK_User_Endereco_EnderecoId",
               table: "User");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Endereco_EnderecoId",
                table: "User",
                column: "EnderecoId",
                principalTable: "Endereco",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
               name: "FK_User_Endereco_EnderecoId",
               table: "User");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Endereco_EnderecoId",
                table: "User",
                column: "EnderecoId",
                principalTable: "Endereco",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
