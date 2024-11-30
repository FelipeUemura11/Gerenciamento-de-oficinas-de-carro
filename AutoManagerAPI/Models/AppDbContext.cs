using AutoManagerAPI.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<CarService> CarServices { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=../AutoManagerAPI/database.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // relação entre Client e Car
        modelBuilder.Entity<Car>()
            .HasOne(c => c.Client)
            .WithMany(c => c.Cars)
            .HasForeignKey(c => c.ClientId);

        // relação entre Order, Client, Car e CarService
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Client)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.ClientId);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Car)
            .WithMany(c => c.OrdersHistoric)
            .HasForeignKey(o => o.CarId);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.CarService)
            .WithMany()
            .HasForeignKey(o => o.CarServiceId);

        modelBuilder.Entity<Client>().HasData(
            new Client
            {
                Id = "63e9fcb4-7b6f-4ac7-b3ec-720b315ed38c",
                Name = "Admin",
                Email = "admin@oficina.up",
                Phone = "41991022117",
                Cpf = "89126811105",
                Cep = "82930230",
                Estado = "PR",
                Cidade = "Curitiba",
                Bairro = "Portao",
                Logradouro = "Rua Jo",
                Senha = "admin123",
                IsAdmin = true
            },
            new Client 
            { 
                Id = "bfe4e7dc-81e4-4e47-a67b-d4fbf3e124bd", 
                Name = "Felipe", 
                Email = "felipe@gmail.com", 
                Phone = "41999999999", 
                Cpf = "12345678900", 
                Cep = "80000000", 
                Estado = "PR", 
                Cidade = "Curitiba", 
                Bairro = "Centro", 
                Logradouro = "Rua das Flores, 123", 
                Senha = "senha123", 
                IsAdmin = false
            },
            new Client 
            { 
                Id = "6d091456-5a2f-4b5a-98fc-f1a3b50a627d", 
                Name = "Icaro", 
                Email = "icaro@gmail.com", 
                Phone = "41988888888", 
                Cpf = "98765432100", 
                Cep = "81000000", 
                Estado = "PR", 
                Cidade = "Curitiba", 
                Bairro = "Batel", 
                Logradouro = "Av. Vicente Machado, 456", 
                Senha = "senha456", 
                IsAdmin = false 
            },
            new Client 
            { 
                Id = "39be53a2-fc09-4b6a-bafa-18a6a23c8f6e", 
                Name = "Rafael", 
                Email = "rafael@gmail.com", 
                Phone = "41977777777", 
                Cpf = "11223344556", 
                Cep = "82000000", 
                Estado = "PR", 
                Cidade = "Curitiba", 
                Bairro = "Santa Felicidade", 
                Logradouro = "Rua Itália, 789", 
                Senha = "senha789", 
                IsAdmin = false 
            });

        modelBuilder.Entity<Car>().HasData(
            new Car
            {
                Id = "c1",
                Brand = "Toyota",
                Model = "Corolla",
                Plate = "ABC1234",
                Year = "2020",
                Color = "Preto",
                ClientId = "bfe4e7dc-81e4-4e47-a67b-d4fbf3e124bd"
            },
            new Car
            {
                Id = "c2",
                Brand = "Honda",
                Model = "Civic",
                Plate = "DEF5678",
                Year = "2019",
                Color = "Prata",
                ClientId = "6d091456-5a2f-4b5a-98fc-f1a3b50a627d"
            },
            new Car
            {
                Id = "c3",
                Brand = "Ford",
                Model = "Fiesta",
                Plate = "GHI9012",
                Year = "2018",
                Color = "Branco",
                ClientId = "39be53a2-fc09-4b6a-bafa-18a6a23c8f6e"
            }
        );
        modelBuilder.Entity<CarService>().HasData(
            new CarService
            {
                Id = "cs1",
                Name = "Troca de Óleo",
                Description = "Substituição de óleo do motor e filtro.",
                Price = 150.00,
                Category = "Manutenção"
            },
            new CarService
            {
                Id = "cs2",
                Name = "Alinhamento e Balanceamento",
                Description = "Serviço para ajuste das rodas e equilíbrio.",
                Price = 120.00,
                Category = "Pneus"
            }
        );

        modelBuilder.Entity<Order>().HasData(
            new Order
            {
                Id = "o1",
                Name = "Troca de óleo para Corolla",
                Description = "Serviço de troca de óleo para o Corolla do Felipe.",
                CarServiceId = "cs1",
                ClientId = "bfe4e7dc-81e4-4e47-a67b-d4fbf3e124bd",
                CarId = "c1",
                TotalPrice = 150.00,
                CreatedDate = DateTime.Now,
                Status = true
            },
            new Order
            {
                Id = "o2",
                Name = "Alinhamento para Civic",
                Description = "Alinhamento e balanceamento do Civic do Admin.",
                CarServiceId = "cs2",
                ClientId = "63e9fcb4-7b6f-4ac7-b3ec-720b315ed38c",
                CarId = "c2",
                TotalPrice = 120.00,
                CreatedDate = DateTime.Now,
                Status = false
            }
        );
    }
}
