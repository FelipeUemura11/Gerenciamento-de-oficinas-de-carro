using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoManagerAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAddressFromClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Clients");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Clients",
                type: "TEXT",
                nullable: true);
        }
    }
}
