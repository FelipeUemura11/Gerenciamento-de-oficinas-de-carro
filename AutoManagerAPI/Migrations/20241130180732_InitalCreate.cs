using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AutoManagerAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitalCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarServices",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    Category = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarServices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: true),
                    Cpf = table.Column<string>(type: "TEXT", nullable: true),
                    Cep = table.Column<string>(type: "TEXT", nullable: true),
                    Estado = table.Column<string>(type: "TEXT", nullable: true),
                    Cidade = table.Column<string>(type: "TEXT", nullable: true),
                    Bairro = table.Column<string>(type: "TEXT", nullable: true),
                    Logradouro = table.Column<string>(type: "TEXT", nullable: true),
                    Senha = table.Column<string>(type: "TEXT", nullable: false),
                    IsAdmin = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Brand = table.Column<string>(type: "TEXT", nullable: false),
                    Model = table.Column<string>(type: "TEXT", nullable: false),
                    Plate = table.Column<string>(type: "TEXT", nullable: false),
                    Year = table.Column<string>(type: "TEXT", nullable: false),
                    Color = table.Column<string>(type: "TEXT", nullable: true),
                    ClientId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    CarServiceId = table.Column<string>(type: "TEXT", nullable: false),
                    ClientId = table.Column<string>(type: "TEXT", nullable: false),
                    CarId = table.Column<string>(type: "TEXT", nullable: false),
                    TotalPrice = table.Column<double>(type: "REAL", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_CarServices_CarServiceId",
                        column: x => x.CarServiceId,
                        principalTable: "CarServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CarServices",
                columns: new[] { "Id", "Category", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { "cs1", "Manutenção", "Substituição de óleo do motor e filtro.", "Troca de Óleo", 150.0 },
                    { "cs2", "Pneus", "Serviço para ajuste das rodas e equilíbrio.", "Alinhamento e Balanceamento", 120.0 }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Bairro", "Cep", "Cidade", "Cpf", "Email", "Estado", "IsAdmin", "Logradouro", "Name", "Phone", "Senha" },
                values: new object[,]
                {
                    { "39be53a2-fc09-4b6a-bafa-18a6a23c8f6e", "Santa Felicidade", "82000000", "Curitiba", "11223344556", "rafael@gmail.com", "PR", false, "Rua Itália, 789", "Rafael", "41977777777", "senha789" },
                    { "63e9fcb4-7b6f-4ac7-b3ec-720b315ed38c", "Portao", "82930230", "Curitiba", "89126811105", "admin@oficina.up", "PR", true, "Rua Jo", "Admin", "41991022117", "admin123" },
                    { "6d091456-5a2f-4b5a-98fc-f1a3b50a627d", "Batel", "81000000", "Curitiba", "98765432100", "icaro@gmail.com", "PR", false, "Av. Vicente Machado, 456", "Icaro", "41988888888", "senha456" },
                    { "bfe4e7dc-81e4-4e47-a67b-d4fbf3e124bd", "Centro", "80000000", "Curitiba", "12345678900", "felipe@gmail.com", "PR", false, "Rua das Flores, 123", "Felipe", "41999999999", "senha123" }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Brand", "ClientId", "Color", "Model", "Plate", "Year" },
                values: new object[,]
                {
                    { "c1", "Toyota", "bfe4e7dc-81e4-4e47-a67b-d4fbf3e124bd", "Preto", "Corolla", "ABC1234", "2020" },
                    { "c2", "Honda", "6d091456-5a2f-4b5a-98fc-f1a3b50a627d", "Prata", "Civic", "DEF5678", "2019" },
                    { "c3", "Ford", "39be53a2-fc09-4b6a-bafa-18a6a23c8f6e", "Branco", "Fiesta", "GHI9012", "2018" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CarId", "CarServiceId", "ClientId", "CreatedDate", "Description", "Name", "Status", "TotalPrice" },
                values: new object[,]
                {
                    { "o1", "c1", "cs1", "bfe4e7dc-81e4-4e47-a67b-d4fbf3e124bd", new DateTime(2024, 11, 30, 15, 7, 31, 991, DateTimeKind.Local).AddTicks(9530), "Serviço de troca de óleo para o Corolla do Felipe.", "Troca de óleo para Corolla", true, 150.0 },
                    { "o2", "c2", "cs2", "63e9fcb4-7b6f-4ac7-b3ec-720b315ed38c", new DateTime(2024, 11, 30, 15, 7, 31, 991, DateTimeKind.Local).AddTicks(9534), "Alinhamento e balanceamento do Civic do Admin.", "Alinhamento para Civic", false, 120.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ClientId",
                table: "Cars",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CarId",
                table: "Orders",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CarServiceId",
                table: "Orders",
                column: "CarServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ClientId",
                table: "Orders",
                column: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "CarServices");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
