using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Restaurant_Services;
using Restaurant_Services.Database;
using Swashbuckle.AspNetCore.Filters;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Dodaj CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:3000") // URL tvoje React aplikacije
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Swagger konfiguracija
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });

        options.OperationFilter<SecurityRequirementsOperationFilter>();
    });

// Dodaj autorizaciju
builder.Services.AddAuthorization();

builder.Services.AddControllers();

// Registracija UserService
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IRolesService, RolesService>();
builder.Services.AddTransient<ITableService, TableService>();
builder.Services.AddTransient<IReservationService, ReservationService>();
builder.Services.AddTransient<ITokenService, TokenService>();

// Registracija AutoMapper-a
builder.Services.AddAutoMapper(typeof(IRolesService));  // Dodaj AutoMapper

// Registracija konteksta baze podataka
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<RestaurantDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddHttpClient();

var app = builder.Build();

// Omogući Swagger uvijek (za sva okruženja)
app.UseSwagger();
app.UseSwaggerUI();

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/swagger");
        return;
    }
    await next();
});

app.UseHttpsRedirection();

// Omogući CORS
app.UseCors("AllowSpecificOrigin"); // Dodaj ovu liniju

// Autorizacija
app.UseAuthorization();

app.MapControllers();

app.Run();
