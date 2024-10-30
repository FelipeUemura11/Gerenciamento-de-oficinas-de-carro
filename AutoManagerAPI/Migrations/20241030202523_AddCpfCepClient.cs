using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoManagerAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddCpfCepClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bairro",
                table: "Clients",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cep",
                table: "Clients",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cidade",
                table: "Clients",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cpf",
                table: "Clients",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Clients",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Logradouro",
                table: "Clients",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "Clients",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bairro",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Cep",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Cidade",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Cpf",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Logradouro",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Senha",
                table: "Clients");
        }
    }
}
