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
    public class NutricionistaAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public NutricionistaAuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<Nutricionista?> RegisterAsync(RegisterNutricionistaDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
                return null;

            if (await _context.Nutricionistas.AnyAsync(u => u.Email == dto.Email))
                return null;

            CustomPasswordHasher.CreatePasswordHash(dto.Password, out var hash, out var salt);

            var nutri = new Nutricionista
            {
                NomeCompleto = dto.NomeCompleto,
                Email = dto.Email,
                CRN = dto.CRN,
                PasswordHash = hash,
                PasswordSalt = salt
            };

            _context.Nutricionistas.Add(nutri);
            await _context.SaveChangesAsync();

            return nutri;
        }

        public async Task<LoginResponseDTO?> LoginAsync(LoginDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
                return null;

            // lockout simples opcional (se tiver a entidade LoginAttempt)
            var attempt = await _context.LoginAttempts.FirstOrDefaultAsync(a => a.Email == dto.Email);
            if (attempt != null && attempt.LockoutEnd.HasValue && attempt.LockoutEnd > DateTime.UtcNow)
                return null;

            var nutri = await _context.Nutricionistas.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (nutri == null || nutri.PasswordHash == null || nutri.PasswordSalt == null)
                return await RegisterFail(dto.Email);

            var isValid = CustomPasswordHasher.VerifyPasswordHash(dto.Password, nutri.PasswordHash, nutri.PasswordSalt);
            if (!isValid) return await RegisterFail(dto.Email);

            if (attempt != null) _context.LoginAttempts.Remove(attempt);
            await _context.SaveChangesAsync();

            var token = GerarTokenJwt(nutri);

            return new LoginResponseDTO
            {
                Token = token,
                Nome = nutri.NomeCompleto,
                Role = "Nutricionista"
            };
        }

        private async Task<LoginResponseDTO?> RegisterFail(string email)
        {
            var attempt = await _context.LoginAttempts.FirstOrDefaultAsync(a => a.Email == email);

            if (attempt == null)
            {
                attempt = new LoginAttempt
                {
                    Email = email,
                    FailedCount = 1,
                    LastAttempt = DateTime.UtcNow
                };
                _context.LoginAttempts.Add(attempt);
            }
            else
            {
                attempt.FailedCount++;
                attempt.LastAttempt = DateTime.UtcNow;

                if (attempt.FailedCount >= 3)
                    attempt.LockoutEnd = DateTime.UtcNow.AddMinutes(5);
            }

            await _context.SaveChangesAsync();
            return null;
        }

        private string GerarTokenJwt(Nutricionista nutricionista)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key ausente no appsettings.");
            var key = Encoding.UTF8.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", nutricionista.Id.ToString()),
                    new Claim(ClaimTypes.Name, nutricionista.NomeCompleto),
                    new Claim(ClaimTypes.Role, "Nutricionista")
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}