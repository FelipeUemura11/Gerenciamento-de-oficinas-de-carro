using AutoManagerAPI.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    // Definindo os DbSets
    public DbSet<Client> Clients { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<CarService> CarServices { get; set; }

    // Opcional: Definindo como o EF Core deve configurar o contexto
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
            optionsBuilder.UseSqlite("Data Source=database.db");
        
    }
}
