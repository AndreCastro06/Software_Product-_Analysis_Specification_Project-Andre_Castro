using PeaceApi.Data;
using PeaceApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddScoped<NutricionistaAuthService>();
builder.Services.AddScoped<PacienteAuthService>();

// JWT config (com checagens de null)
var jwtKey = builder.Configuration["Jwt:Key"]
    ?? throw new ArgumentNullException("Jwt:Key is missing in configuration");

var jwtIssuer = builder.Configuration["Jwt:Issuer"]
    ?? throw new ArgumentNullException("Jwt:Issuer is missing in configuration");

var jwtAudience = builder.Configuration["Jwt:Audience"]
    ?? throw new ArgumentNullException("Jwt:Audience is missing in configuration");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ClockSkew = TimeSpan.Zero // expiração sem tolerância
        };
    });

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connStr = builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    options.UseNpgsql(connStr);
});

// CORS (ajuste os domínios conforme seu front)



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
                "http://localhost:5173",
                "https://localhost:5174",
                "https://localhost:5175"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
       
    });
});

builder.Services.AddControllers();
builder.Services.AddAuthorization();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger deve chamar UseSwagger() ANTES do UseSwaggerUI()
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Peace API V1");
    c.RoutePrefix = string.Empty; // Swagger na raiz
});

app.UseCors("AllowFrontend");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();