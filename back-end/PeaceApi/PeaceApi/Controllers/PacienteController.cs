using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PeaceApi.Data;
using PeaceApi.DTOs;
using PeaceApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PeaceApi.Controllers
{
    [ApiController]
    [Route("api/public/paciente")]
    public class PublicPacienteController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public PublicPacienteController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<Paciente>> Register(RegisterPacienteDTO request)
        {
            if (await _context.Pacientes.AnyAsync(p => p.Email == request.Email))
                return BadRequest("Paciente já registrado com este email.");

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var paciente = new Paciente
            {
                NomeCompleto = request.NomeCompleto,
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                NutricionistaId = null
            };



            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Register), new { id = paciente.Id }, paciente);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginDTO request)
        {
            var paciente = await _context.Pacientes.FirstOrDefaultAsync(p => p.Email == request.Email);
            if (paciente == null)
                return Unauthorized("Paciente não encontrado.");

            if (!VerifyPasswordHash(request.Password, paciente.PasswordHash, paciente.PasswordSalt))
                return Unauthorized("Senha incorreta.");

            string token = CreateToken(paciente);
            return Ok(token);
        }

        private string CreateToken(Paciente paciente)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, paciente.Id.ToString()),
                new Claim(ClaimTypes.Email, paciente.Email),
                new Claim(ClaimTypes.Role, "Paciente")
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