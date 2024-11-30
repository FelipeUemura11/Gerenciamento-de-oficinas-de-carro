using System;
using AutoManagerAPI.Models;
using Microsoft.EntityFrameworkCore;

class Program
{
    static void Main(string[] args)
    {
        using (var context = new AppDbContext())
        {
            while (true)
            {
                Console.WriteLine("Admin Menu:");
                Console.WriteLine("------------- CAR SERVICES -------------");
                Console.WriteLine("1. Add CarService");
                Console.WriteLine("2. Remove CarService");
                Console.WriteLine("3. List All CarServices");
                Console.WriteLine("------------- CLIENTS -------------");
                Console.WriteLine("4. Add Client/User");
                Console.WriteLine("5. Remove Client/User");
                Console.WriteLine("6. List All Clients");
                Console.WriteLine("------------- DATABASE -------------");
                Console.WriteLine("7. Reset Database");
                Console.WriteLine("8. Exit");
                Console.Write("Select an option: ");
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        AddCarService(context);
                        break;
                    case "2":
                        RemoveCarService(context);
                        break;
                    case "3":
                        ListAllCarServices(context);
                        break;
                    case "4":
                        AddClient(context);
                        break;
                    case "5":
                        RemoveClient(context);
                        break;
                    case "6":
                        ListAllClients(context);
                        break;
                    case "7":
                        ResetDatabase(context);
                        break;
                    case "8":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }
    }

    static void AddCarService(AppDbContext context)
    {
        Console.Write("Enter CarService Name: ");
        var name = Console.ReadLine();
        Console.Write("Enter CarService Price: ");
        var price = double.Parse(Console.ReadLine());
        Console.Write("Enter CarService Category: ");
        var category = Console.ReadLine();
        Console.Write("Enter CarService Description: ");
        var description = Console.ReadLine();

        var carService = new CarService 
        { 
            Id = Guid.NewGuid().ToString(), 
            Name = name, 
            Price = price, 
            Category = category, 
            Description = description 
        };
        context.CarServices.Add(carService);
        context.SaveChanges();
        Console.WriteLine("CarService added successfully.");
    }

    static void RemoveCarService(AppDbContext context)
    {
        Console.Write("Enter CarService ID to remove: ");
        var id = Console.ReadLine();

        var carService = context.CarServices.Find(id);
        if (carService == null)
        {
            Console.WriteLine("CarService not found.");
            return;
        }

        context.CarServices.Remove(carService);
        context.SaveChanges();
        Console.WriteLine("CarService removed successfully.");
    }

    static void ListAllCarServices(AppDbContext context)
    {
        var carServices = context.CarServices.ToList();
        if (!carServices.Any())
        {
            Console.WriteLine("No CarServices found.");
            return;
        }

        Console.WriteLine("CarServices:");
        foreach (var carService in carServices)
        {
            Console.WriteLine($"ID: {carService.Id}, Name: {carService.Name}, Price: {carService.Price}, Category: {carService.Category}, Description: {carService.Description}");
        }
    }

    static void AddClient(AppDbContext context)
    {
        Console.Write("Enter Client Name: ");
        var name = Console.ReadLine();
        Console.Write("Enter Client Email: ");
        var email = Console.ReadLine();
        Console.Write("Enter Client Phone: ");
        var phone = Console.ReadLine();
        Console.Write("Enter Client CPF: ");
        var cpf = Console.ReadLine();
        Console.Write("Enter Client CEP: ");
        var cep = Console.ReadLine();
        Console.Write("Enter Client Estado: ");
        var estado = Console.ReadLine();
        Console.Write("Enter Client Cidade: ");
        var cidade = Console.ReadLine();
        Console.Write("Enter Client Bairro: ");
        var bairro = Console.ReadLine();
        Console.Write("Enter Client Logradouro: ");
        var logradouro = Console.ReadLine();
        Console.Write("Enter Client Senha: ");
        var senha = Console.ReadLine();
        Console.Write("Enter Client IsAdmin (true): ");
        var isAdmin = Console.ReadLine() == "true";

        var client = new Client
        {
            Id = Guid.NewGuid().ToString(),
            Name = name,
            Email = email,
            Phone = phone,
            Cpf = cpf,
            Cep = cep,
            Estado = estado,
            Cidade = cidade,
            Bairro = bairro,
            Logradouro = logradouro,
            Senha = senha,
            IsAdmin = isAdmin
        };
        context.Clients.Add(client);
        context.SaveChanges();
        Console.WriteLine("Client added successfully.");
    }

    static void RemoveClient(AppDbContext context)
    {
        Console.Write("Enter Client ID to remove: ");
        var id = Console.ReadLine();

        var client = context.Clients.Find(id);
        if (client == null)
        {
            Console.WriteLine("Client not found.");
            return;
        }

        context.Clients.Remove(client);
        context.SaveChanges();
        Console.WriteLine("Client removed successfully.");
    }

    static void ListAllClients(AppDbContext context)
    {
        var clients = context.Clients.ToList();
        if (!clients.Any())
        {
            Console.WriteLine("No Clients found.");
            return;
        }

        Console.WriteLine("Clients:");
        foreach (var client in clients)
        {
            Console.WriteLine($"ID: {client.Id}, Name: {client.Name}, Email: {client.Email}, Phone: {client.Phone}, CPF: {client.Cpf}, CEP: {client.Cep}, Estado: {client.Estado}, Cidade: {client.Cidade}, Bairro: {client.Bairro}, Logradouro: {client.Logradouro}");
        }
    }

    static void ResetDatabase(AppDbContext context)
    {
        context.Database.ExecuteSqlRaw("DELETE FROM Orders");
        context.Database.ExecuteSqlRaw("DELETE FROM Cars");
        context.Database.ExecuteSqlRaw("DELETE FROM Clients");
        context.Database.ExecuteSqlRaw("DELETE FROM CarServices");
        context.SaveChanges();
        Console.WriteLine("Database reset successfully.");
    }
}
