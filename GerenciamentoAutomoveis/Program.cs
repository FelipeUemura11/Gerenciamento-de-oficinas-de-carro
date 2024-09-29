var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "hELLO AUTOMOVEIS");

app.Run();
