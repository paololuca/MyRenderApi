using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My Render API",
        Version = "v1"
    });
});

var app = builder.Build();

// Swagger UI
if (app.Environment.IsDevelopment() || true) // true → attivo anche in produzione
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Endpoint di esempio
app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});
app.MapGet("/ready", () => "API pronta 🚀");
app.MapGet("/helloworld", () => new { Message = "Hello from Render!" })
   .WithName("GetWeather")
   .WithTags("Demo");

app.Run();
