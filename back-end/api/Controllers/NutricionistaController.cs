using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PEACE.api.Data;
using PEACE.api.DTOs;
using PEACE.api.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PEACE.api.Controllers
{
    [ApiController]
    [Route("api/public/nutricionista")]
    public class PublicNutricionistaController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public PublicNutricionistaController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<Nutricionista>> Register(RegisterNutricionistaDTO request)
        {
            if (await _context.Nutricionistas.AnyAsync(n => n.Email == request.Email))
                return BadRequest("Nutricionista já registrado com este email.");

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var nutricionista = new Nutricionista
            {
                NomeCompleto= request.NomeCompleto!,
                Email = request.Email!,
                CRN = request.CRN!,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
                
            };

            _context.Nutricionistas.Add(nutricionista);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Register), new { id = nutricionista.Id }, nutricionista);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginDTO request)
        {
            var nutricionista = await _context.Nutricionistas.FirstOrDefaultAsync(n => n.Email == request.Email);
            if (nutricionista == null) return Unauthorized("Nutricionista não encontrado.");

            if (!VerifyPasswordHash(request.Password, nutricionista.PasswordHash, nutricionista.PasswordSalt))
                return Unauthorized("Senha incorreta.");

            string token = CreateToken(nutricionista);
            return Ok(token);
        }

        private string CreateToken(Nutricionista nutricionista)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, nutricionista.Id.ToString()),
                new Claim(ClaimTypes.Email, nutricionista.Email),
                new Claim(ClaimTypes.Role, "Nutricionista")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(12),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
        {
            using var hmac = new HMACSHA512();
            salt = hmac.Key;
            hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPasswordHash(string password, byte[] hash, byte[] salt)
        {
            using var hmac = new HMACSHA512(salt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(hash);

        }
    }
}
