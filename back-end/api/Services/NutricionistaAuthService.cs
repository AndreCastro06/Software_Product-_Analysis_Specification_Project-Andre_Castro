using Microsoft.EntityFrameworkCore;
using PEACE.api.Data;
using PEACE.api.DTOs;
using PEACE.api.Models;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace PEACE.api.Services
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
            if (await _context.Nutricionistas.AnyAsync(u => u.Email == dto.Email))
                return null;

            PasswordHasher.CreatePasswordHash(dto.Password, out byte[] hash, out byte[] salt);

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
            var attempt = await _context.LoginAttempts.FirstOrDefaultAsync(a => a.Email == dto.Email);

            if (attempt != null && attempt.LockoutEnd.HasValue && attempt.LockoutEnd > DateTime.UtcNow)
                return null; // Bloqueado temporariamente

            var nutri = await _context.Nutricionistas.FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (nutri == null || nutri.PasswordHash == null || nutri.PasswordSalt == null)
                return await RegisterFail(dto.Email);

            bool isValid = PasswordHasher.VerifyPasswordHash(dto.Password, nutri.PasswordHash, nutri.PasswordSalt);

            if (!isValid)
                return await RegisterFail(dto.Email);

            // Login bem-sucedido → limpa tentativas
            if (attempt != null)
                _context.LoginAttempts.Remove(attempt);

            await _context.SaveChangesAsync();

            // ✅ Gera Token JWT com NameIdentifier
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
                    attempt.LockoutEnd = DateTime.UtcNow.AddMinutes(5); // bloqueia por 5 minutos
            }

            await _context.SaveChangesAsync();
            return null;
        }

        // ✅ Corrigido: inclui ClaimTypes.NameIdentifier
        private string GerarTokenJwt(Nutricionista nutricionista)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, nutricionista.Id.ToString()), // usado no Controller
                new Claim(ClaimTypes.Name, nutricionista.NomeCompleto),
                new Claim(ClaimTypes.Role, "Nutricionista")
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}