using Integrador.Web.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureStartupConfiguration(builder.Configuration);
var app = builder.Build().UseStartupConfiguration(builder.Environment);

app.Run();