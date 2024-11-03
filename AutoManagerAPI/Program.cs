
// NOTA by @icarogabriel: Este código ainda não está otimizado. Em breve, irei compactar os processos de DTO e mapeamento de entidades.

using AutoManagerAPI.Models;
using Microsoft.EntityFrameworkCore;
using AutoManagerAPI.dto;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();
app.UseCors("AllowAll");
// Metodo HTTP para acesso ao login do usuario
app.MapPost("/login", async (LoginDto loginDto, AppDbContext context) =>
{
    var user = await context.Clients.FirstOrDefaultAsync(c => c.Email == loginDto.Email && c.Senha == loginDto.Senha);
    return user != null ? Results.Ok("Login realizado com sucesso!") : Results.Unauthorized();
});

// MÉTODOS AUXILIARES DE DTO
ClientDetailDto MapToClientDetailDto(Client client)
{
    return new ClientDetailDto
    {
        Id = client.Id,
        Name = client.Name,
        Email = client.Email,
        Phone = client.Phone,
        Cpf = client.Cpf,
        Cep = client.Cep,
        Estado = client.Estado,
        Cidade = client.Cidade,
        Bairro = client.Bairro,
        Logradouro = client.Logradouro,
        Senha = client.Senha,
        Cars = client.Cars.Select(car => new CarDto
        {
            Id = car.Id,
            Brand = car.Brand,
            Model = car.Model,
            Year = car.Year,
            Plate = car.Plate,
            Color = car.Color
        }).ToList(),
        Orders = client.Orders.Select(order => MapToOrderDto(order)).ToList()
    };
}

ClientDto MapToClientDto(Client client)
{
    return new ClientDto
    {
        Id = client.Id,
        Name = client.Name,
        Email = client.Email,
        Phone = client.Phone,
        Cpf = client.Cpf,
        Cep = client.Cep,
        Estado = client.Estado,
        Cidade = client.Cidade,
        Bairro = client.Bairro,
        Logradouro = client.Logradouro,
        Senha = client.Senha,
    };
}

CarDto MapToCarDto(Car car)
{
    return new CarDto
    {
        Id = car.Id,
        Brand = car.Brand,
        Model = car.Model,
        Year = car.Year,
        Plate = car.Plate,
        Color = car.Color
    };
}

OrderDto MapToOrderDto(Order order)
{
    return new OrderDto
    {
        Id = order.Id,
        Name = order.Name,
        ClientId = order.ClientId,
        Client = MapToClientDto(order.Client),
        CarId = order.CarId,
        Car = MapToCarDto(order.Car),
        CarServiceId = order.CarServiceId,
        CarService = order.CarService,
        TotalPrice = order.TotalPrice,
        CreatedDate = order.CreatedDate,
        Status = order.Status
    };
}

OrderNoCarDto MapToOrderNoCarDto(Order order)
{
    return new OrderNoCarDto
    {
        Id = order.Id,
        Name = order.Name,
        CarServiceId = order.CarServiceId,
        TotalPrice = order.TotalPrice,
        CreatedDate = order.CreatedDate,
        Status = order.Status
    };
}

// ------------------------------------------------- SERVIÇOS DA OFICINA -------------------------------------------------
// Criar novo Serviço
app.MapPost("/carservice", async (CarService carservice, AppDbContext context) =>
{
    context.Add(carservice);
    await context.SaveChangesAsync();
    return Results.Created($"/carservice/{carservice.Id}", carservice);
});

// Retornar todos os Serviços do DB
app.MapGet("/carservices", async (AppDbContext context) =>
{
    var carservices = await context.CarServices.ToListAsync();
    return Results.Ok(carservices);
});

// Retornar serviço por nome
app.MapGet("/carservice/{name}", async (string name, AppDbContext context) =>
{
    var carservice = await context.CarServices.Where(c => c.Name.Contains(name)).ToListAsync();
    return carservice.Any() ? Results.Ok(carservice) : Results.NotFound("Nenhum serviço encontrado.");
});

// Remover serviço
app.MapDelete("/carservices/{id}", async (string id, AppDbContext context) =>
{
    var carservice = await context.CarServices.FindAsync(id);
    if(carservice == null)
    {
        return Results.NotFound("Serviço não encontrado.");
    }

    // Remove todas as ordens relacionadas ao serviço
    var ordersToRemove = context.Orders
        .Include(o => o.Car)
        .Include(o => o.Client)
        .Where(order => order.CarServiceId == id)
        .ToList();

    foreach (var order in ordersToRemove)
    {
        // Remove a ordem do histórico do carro
        order.Car.OrdersHistoric.Remove(order);

        // Remove a ordem da lista do cliente
        order.Client.Orders.Remove(order);
    }

    context.Orders.RemoveRange(ordersToRemove);

    // Remove o serviço
    context.CarServices.Remove(carservice);
    await context.SaveChangesAsync();
    return Results.Ok("Serviço removido com sucesso.");
}
);

// ------------------------------------------------- CLIENTES -------------------------------------------------
// Criar novo Cliente
app.MapPost("/client", async (Client client, AppDbContext context) =>
{
    context.Add(client);
    await context.SaveChangesAsync();
    return Results.Created($"/client/{client.Id}", client);
}
);

// Retornar todos os clientes
app.MapGet("/clients", async (AppDbContext context) => 
{
    var clients = await context.Clients
        .Include(c => c.Cars)
        .Include(c => c.Orders)
        .ThenInclude(order => order.Car)
        .Include(c => c.Orders)
        .ThenInclude(order => order.CarService)
        .ToListAsync();

    var clientsWithDetailDto = clients.Select(client => MapToClientDetailDto(client)).ToList();
    
    return Results.Ok(clientsWithDetailDto);
}
);

// Retornar cliente pelo id
// @nota: utilizado somente para ver detalhes do cliente dentro da página
app.MapGet("client/{id}", async (string id, AppDbContext context) => 
{
    var client = await context.Clients
        .Include(c => c.Cars)
        .Include(c => c.Orders)
        .ThenInclude(order => order.Car)
        .Include(c => c.Orders)
        .ThenInclude(order => order.CarService)
        .FirstOrDefaultAsync(c => c.Id == id);
    
    if (client == null)
    {
        return Results.NotFound("Cliente não encontrado.");
    }

    var clientDetailDto = MapToClientDetailDto(client);
    return Results.Ok(clientDetailDto);
}
);

// Atualizar cliente
app.MapPut("/clients/{id}", async (string id, Client updatedClient, AppDbContext context) =>
{
    var client = await context.Clients.FindAsync(id);
    if(client == null)
    {
        return Results.NotFound("Cliente não encontrado.");
    }

    client.Name = updatedClient.Name;
    client.Email = updatedClient.Email;
    client.Phone = updatedClient.Phone;
    await context.SaveChangesAsync();
    return Results.Ok("Cliente atualizado com sucesso.");
});

// Remover cliente
app.MapDelete("/clients/{id}", async (string id, AppDbContext context) => 
{
    var client = await context.Clients
        .Include(c => c.Cars) // Inclui os carros do cliente
        .Include(c => c.Orders) // Inclui as ordens do cliente
        .FirstOrDefaultAsync(c => c.Id == id);
    
    if (client == null)
    {
        return Results.NotFound("Cliente não encontrado.");
    }

    // Remove todas as ordens relacionadas ao cliente
    var ordersToRemove = context.Orders.Where(order => order.ClientId == id).ToList();
    context.Orders.RemoveRange(ordersToRemove);

    // Remove todos os carros relacionados ao cliente
    var carsToRemove = context.Cars.Where(car => car.ClientId == id).ToList();
    context.Cars.RemoveRange(carsToRemove);

    // Remove o cliente
    context.Clients.Remove(client);
    await context.SaveChangesAsync();
    return Results.Ok("Cliente excluído com sucesso.");
});

// ------------------------------------------------- CARROS -------------------------------------------------
app.MapPost("/car", async (Car car, AppDbContext context) =>
{
    var clientId = car.ClientId;
    var client = await context.Clients.FindAsync(clientId);
    if (client == null)
    {
        return Results.NotFound("Cliente não encontrado.");
    }

    client.Cars.Add(car);
    car.Client = client;

    context.Add(car);
    await context.SaveChangesAsync();

    var clientDto = MapToClientDto(client);
    var carWithClientDto = new
    {
        car.Id,
        car.Brand,
        car.Model,
        car.Year,
        car.Plate,
        car.Color,
        Client = clientDto,
        car.ClientId,
        car.OrdersHistoric
    };

    return Results.Created($"/car/{car.Id}", carWithClientDto);
});

app.MapGet("/cars", async (AppDbContext context) =>
{
    var cars = await context.Cars
        .Include(c => c.Client)
        .Include(c => c.OrdersHistoric)
        .ToListAsync();

    var carsWithClientDto = cars.Select(car =>
    {
        var clientDto = MapToClientDto(car.Client);
        var ordersHistoricDto = car.OrdersHistoric.Select(order => MapToOrderNoCarDto(order)).ToList();
        return new
        {
            car.Id,
            car.Brand,
            car.Model,
            car.Year,
            car.Plate,
            car.Color,
            Client = clientDto,
            car.ClientId,
            OrdersHistoric = ordersHistoricDto
        };
    });
    return Results.Ok(carsWithClientDto);
});

app.MapGet("car/{id}", async (string id, AppDbContext context) => 
{
    var car = await context.Cars
        .Include(c => c.Client)
        .Include(c => c.OrdersHistoric)
        .FirstOrDefaultAsync(c => c.Id == id);
    if (car == null)
    {
        return Results.NotFound("Carro não encontrado.");
    }

    var clientDto = MapToClientDto(car.Client);
    var ordersHistoricDto = car.OrdersHistoric.Select(order => MapToOrderNoCarDto(order)).ToList();
    var carWithClientDto = new
    {
        car.Id,
        car.Brand,
        car.Model,
        car.Year,
        car.Plate,
        car.Color,
        Client = clientDto,
        car.ClientId,
        OrdersHistoric = ordersHistoricDto
    };

    return Results.Ok(carWithClientDto);
}
);

app.MapDelete("/cars/{id}", async (string id, AppDbContext context) => 
{
    var car = await context.Cars
        .Include(c => c.Client)
        .Include(c => c.OrdersHistoric) // Inclui as ordens históricas do carro
        .FirstOrDefaultAsync(c => c.Id == id);
    
    if (car == null)
    {
        return Results.NotFound("Carro não encontrado.");
    }

    // Remove todas as ordens relacionadas ao carro
    var ordersToRemove = context.Orders.Where(order => order.CarId == id).ToList();
    foreach (var order in ordersToRemove)
    {
        // Remove a ordem da lista do cliente
        order.Client.Orders.Remove(order);
    }
    context.Orders.RemoveRange(ordersToRemove);

    // Remove o carro da lista de carros do cliente
    car.Client.Cars.Remove(car);

    // Remove o carro
    context.Cars.Remove(car);
    await context.SaveChangesAsync();
    return Results.Ok("Carro excluído com sucesso.");
});

// ------------------------------------------------- ORDENS --------------------------------------------------
// Criar nova ordem
// @notas:
// - O cliente e o carro devem ser cadastrados antes de criar uma ordem
// - O carro deve pertencer ao cliente
// - Ao cadastrar a ordem, a ordem deve ser adicionada às seguintes listas: OrdersHistoric do carro e Orders do cliente
// - A ordem tem relação estrangeira com: Client, Car e CarService
// - O preço total da ordem é o preço do serviço do carro

app.MapPost("/order", async (Order order, AppDbContext context) =>
{
    var clientId = order.ClientId;
    var client = await context.Clients.FindAsync(clientId);
    if (client == null)
    {
        return Results.NotFound("Cliente não encontrado.");
    }

    var carId = order.CarId;
    var car = await context.Cars.FindAsync(carId);
    if (car == null)
    {
        return Results.NotFound("Carro não encontrado.");
    }

    if (car.ClientId != clientId)
    {
        return Results.BadRequest("O carro não pertence ao cliente.");
    }

    var carServiceId = order.CarServiceId;
    var carService = await context.CarServices.FindAsync(carServiceId);
    if (carService == null)
    {
        return Results.NotFound("Serviço não encontrado.");
    }

    order.Car = car;
    order.Client = client;
    order.CarService = carService;
    order.TotalPrice = carService.Price;

    car.OrdersHistoric.Add(order);
    client.Orders.Add(order);

    context.Orders.Add(order);
    await context.SaveChangesAsync();

    var clientDto = MapToClientDto(client);
    var carDto = MapToCarDto(car);

    var orderDto = new
    {
        order.Id,
        order.Name,
        order.Description,
        CarService = carService,
        TotalPrice = order.TotalPrice,
        order.CreatedDate,
        order.Status,
        Client = clientDto,
        Car = carDto
    };

    return Results.Created($"/order/{order.Id}", orderDto);
});

// Obter todas as ordens
app.MapGet("/orders", async (AppDbContext context) =>
{
    var orders = await context.Orders
        .Include(o => o.Client)
        .Include(o => o.Car)
        .Include(o => o.CarService)
        .ToListAsync();

    var ordersDto = orders.Select(order =>
    {
        var clientDto = MapToClientDto(order.Client);
        var carDto = MapToCarDto(order.Car);

        return new
        {
            order.Id,
            order.Name,
            order.Description,
            CarService = order.CarService,
            TotalPrice = order.TotalPrice,
            order.CreatedDate,
            order.Status,
            Client = clientDto,
            Car = carDto
        };
    });

    return Results.Ok(ordersDto);
});

// Obter ordem por id
app.MapGet("/order/{id}", async (string id, AppDbContext context) =>
{
    var order = await context.Orders
        .Include(o => o.Client)
        .Include(o => o.Car)
        .Include(o => o.CarService)
        .FirstOrDefaultAsync(o => o.Id == id);
    if (order == null)
    {
        return Results.NotFound("Ordem não encontrada.");
    }

    var clientDto = MapToClientDto(order.Client);
    var carDto = MapToCarDto(order.Car);

    var orderDto = new
    {
        order.Id,
        order.Name,
        order.Description,
        order.CarService,
        order.TotalPrice,
        order.CreatedDate,
        order.Status,
        Client = clientDto,
        Car = carDto
    };

    return Results.Ok(orderDto);
});

// Atualizar ordem de false (não concluída) para true (concluída) - Order não terá DELETE, ela ficará registrada sempre no histórico
app.MapPut("/orders/status/{id}", async (string id, AppDbContext context) =>
{
    var order = await context.Orders.FindAsync(id);
    if (order == null)
    {
        return Results.NotFound("Ordem não encontrada.");
    }
    if (order.Status == true)
    {
        return Results.BadRequest("Esta ordem já foi concluída.");
    }

    order.Status = true;
    await context.SaveChangesAsync();
    return Results.Ok("Ordem concluída com sucesso.");
});

app.Run();
// LoginDto serve como um objeto que carrega os dados do login (email e senha) entre o cliente (frontend) e o servidor.
public record LoginDto(string Email, string Senha);
