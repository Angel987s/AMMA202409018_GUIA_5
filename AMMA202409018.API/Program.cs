using AMMA202409018.API.Endpoints;
using AMMA202409018.API.Models.DAL;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

// Crea un nuevo constructor de la aplicación web.
var builder = WebApplication.CreateBuilder(args);

// Agrega servicios para habilitar la generación de documentación de API.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura y agrega un contexto de base de datos para Entity Framework Core.
builder.Services.AddDbContext<AMMACRMContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Conn"));
});

// Agrega una instancia de la clase CustomerDAL como un servicio para la inyección de dependencias.
builder.Services.AddScoped<ProductAMMADAL>();

// Construye la aplicación web.
var app = builder.Build();

// Agrega los puntos finales relacionados con los clientes a la aplicación.
app.AddProductEndpoints();

// Verifica si la aplicación se está ejecutando en un entorno de desarrollo.
if (app.Environment.IsDevelopment())
{
    // Habilita el uso de Swagger para la documentación de la API.
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Agrega middleware para redirigir las solicitudes HTTP a HTTPS.
app.UseHttpsRedirection();

// Ejecuta la aplicación.
app.Run();