using AutoManagerAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>();
var app = builder.Build();

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

// Atualizar serviço
app.MapPut("/carservices/{id}", async (string id, CarService updatedCarService, AppDbContext context) =>
{
    var carservice = await context.CarServices.FindAsync(id);
    if(carservice == null)
    {
        return Results.NotFound("Serviço não encontrado.");
    }

    carservice.Name = updatedCarService.Name;
    carservice.Description = updatedCarService.Description;
    carservice.Price = updatedCarService.Price;
    carservice.Category = updatedCarService.Category;
    await context.SaveChangesAsync();
    return Results.Ok("Produto atualizado com sucesso.");
});

// Remover serviço
app.MapDelete("/carservices/{id}", async (string id, AppDbContext context) =>
{
    var carservice = await context.CarServices.FindAsync(id);
    if(carservice == null)
    {
        return Results.NotFound("Serviço não encontrado.");
    }
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
    var clients = await context.Clients.ToListAsync();
    return Results.Ok(clients);
}
);

// Retorna cliente pelo id
// @nota: utilizado somente para ver detalhes do cliente dentro da página
app.MapGet("client/{id}", async (string id, AppDbContext context) => 
{
    var client = await context.Clients.FindAsync(id);
    return client != null ? Results.Ok(client) : Results.NotFound("Cliente não encontrado.");
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
    client.Address = updatedClient.Address;
    await context.SaveChangesAsync();
    return Results.Ok("Cliente atualizado com sucesso.");
});

// Remover cliente
app.MapDelete("/clients/{id}", async (string id, AppDbContext context) => 
{
    var client = await context.Clients.FindAsync(id);
    if(client == null)
    {
        return Results.NotFound("Cliente não encontrado.");
    }
    context.Clients.Remove(client);
    await context.SaveChangesAsync();
    return Results.Ok("Cliente excluído com sucesso.");
});

app.Run();
