

using Microsoft.EntityFrameworkCore;
using PeaceApi.Data;
using PeaceApi.DTOs;
using PeaceApi.Models;
using PeaceApi.Services;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace PeaceApi.Services
{
    public class PacienteAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public PacienteAuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<Paciente?> RegisterAsync(RegisterPacienteDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
                return null;

            if (await _context.Pacientes.AnyAsync(p => p.Email == dto.Email))
                return null;

            CustomPasswordHasher.CreatePasswordHash(dto.Password, out var hash, out var salt);

            var paciente = new Paciente
            {
                NomeCompleto = dto.NomeCompleto,
                Email = dto.Email,
                PasswordHash = hash,
                PasswordSalt = salt,
                NutricionistaId = null
            };

            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();

            return paciente;
        }

        public async Task<LoginResponseDTO?> LoginAsync(LoginDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
                return null;

            var paciente = await _context.Pacientes.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (paciente == null || paciente.PasswordHash == null || paciente.PasswordSalt == null)
                return null;

            var isValid = CustomPasswordHasher.VerifyPasswordHash(dto.Password, paciente.PasswordHash, paciente.PasswordSalt);
            if (!isValid) return null;

            var token = GerarTokenJwt(paciente);

            return new LoginResponseDTO
            {
                Token = token,
                Nome = paciente.NomeCompleto,
                Role = "Paciente"
            };
        }

        private string GerarTokenJwt(Paciente paciente)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key ausente no appsettings.");
            var key = Encoding.UTF8.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", paciente.Id.ToString()),
                    new Claim(ClaimTypes.Name, paciente.NomeCompleto),
                    new Claim(ClaimTypes.Role, "Paciente")
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
