using PEACE.api.Data;
using PEACE.api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Logging para debug
builder.Logging.AddConsole();

// Serviços
builder.Services.AddScoped<NutricionistaAuthService>();
builder.Services.AddScoped<AvaliacaoAntropometricaService>();
builder.Services.AddScoped<AnamneseService>();

// JWT
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"]
    ?? throw new ArgumentNullException("Jwt:Issuer missing");
var jwtAudience = builder.Configuration["Jwt:Audience"]
    ?? throw new ArgumentNullException("Jwt:Audience missing");

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"JWT inválido: {context.Exception.Message}");
                Console.ResetColor();
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                var name = context.Principal?.Identity?.Name ?? "(sem nome)";
                var role = context.Principal?.FindFirst("role")?.Value ?? "sem role";
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Token válido para {name} ({role})");
                Console.ResetColor();
                return Task.CompletedTask;
            }
        };
    });

// Banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connStr = builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    options.UseNpgsql(connStr);
});

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
                "http://localhost:5173",
                "https://localhost:5173",
                "https://localhost:5174",
                "https://localhost:5175")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();