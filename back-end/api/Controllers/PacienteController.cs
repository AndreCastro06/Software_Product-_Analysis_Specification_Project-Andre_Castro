using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PEACE.api.Data;
using PEACE.api.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace PEACE.api.Controllers
{
    [ApiController]
    [Route("api/pacientes")]
    [Authorize(Roles = "Nutricionista")]
    public class PacienteController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PacienteController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] Paciente dto)
        {
            var claimId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (claimId == null)
                return Unauthorized("Token invalido.");

            int nutriId = int.Parse(claimId);

            bool nomeExiste = await _context.Pacientes
                .AnyAsync(p => p.NutricionistaId == nutriId &&
                               p.NomeCompleto.ToLower() == dto.NomeCompleto.ToLower());

            if (nomeExiste)
                return Conflict("Ja existe um paciente com esse nome cadastrado por voce.");

            var paciente = new Paciente
            {
                NomeCompleto = dto.NomeCompleto.Trim(),
                DataNascimento = dto.DataNascimento,
                NutricionistaId = nutriId
            };

            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObterPorId), new { id = paciente.Id }, new
            {
                message = "Paciente criado com sucesso.",
                paciente.Id,
                paciente.NomeCompleto,
                paciente.DataNascimento
            });
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var claimId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (claimId == null)
                return Unauthorized("Token invalido.");

            int nutriId = int.Parse(claimId);

            var pacientes = await _context.Pacientes
                .Where(p => p.NutricionistaId == nutriId)
                .OrderBy(p => p.NomeCompleto)
                .ToListAsync();

            return Ok(pacientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
                return NotFound("Paciente nao encontrado.");

            return Ok(paciente);
        }
    }
}


//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using PEACE.api.Data;
//using PEACE.api.DTOs;
//using PEACE.api.Models;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Security.Cryptography;
//using System.Text;

//namespace PEACE.api.Controllers
//{
//    [ApiController]
//    [Route("api/public/paciente")]
//    public class PublicPacienteController : ControllerBase
//    {
//        private readonly AppDbContext _context;
//        private readonly IConfiguration _configuration;

//        public PublicPacienteController(AppDbContext context, IConfiguration configuration)
//        {
//            _context = context;
//            _configuration = configuration;
//        }

//        [HttpPost("register")]
//        public async Task<ActionResult<Paciente>> Register(RegisterPacienteDTO request)
//        {
//            if (await _context.Pacientes.AnyAsync(p => p.Email == request.Email))
//                return BadRequest("Paciente já registrado com este email.");

//            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

//            var paciente = new Paciente
//            {
//                NomeCompleto = request.NomeCompleto,
//                Email = request.Email,
//                PasswordHash = passwordHash,
//                PasswordSalt = passwordSalt,
//                NutricionistaId = null
//            };

//            _context.Pacientes.Add(paciente);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction(nameof(Register), new { id = paciente.Id }, paciente);
//        }

//        [HttpPost("login")]
//        public async Task<ActionResult<string>> Login(LoginDTO request)
//        {
//            var paciente = await _context.Pacientes.FirstOrDefaultAsync(p => p.Email == request.Email);
//            if (paciente == null)
//                return Unauthorized("Paciente não encontrado.");

//            if (!VerifyPasswordHash(request.Password, paciente.PasswordHash, paciente.PasswordSalt))
//                return Unauthorized("Senha incorreta."); 

//            string token = CreateToken(paciente);
//            return Ok(token);
//        }

//        private string CreateToken(Paciente paciente)
//        {
//            List<Claim> claims = new()
//            {
//                new Claim(ClaimTypes.NameIdentifier, paciente.Id.ToString()),
//                new Claim(ClaimTypes.Email, paciente.Email),
//                new Claim(ClaimTypes.Role, "Paciente")
//            };

//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
//            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
//            var token = new JwtSecurityToken(
//                issuer: _configuration["Jwt:Issuer"],
//                audience: _configuration["Jwt:Audience"],
//                claims: claims,
//                expires: DateTime.Now.AddHours(12),
//                signingCredentials: creds);

//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }

//        private void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
//        {
//            using var hmac = new HMACSHA512();
//            salt = hmac.Key;
//            hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
//        }

//        private bool VerifyPasswordHash(string password, byte[] hash, byte[] salt)
//        {
//            using var hmac = new HMACSHA512(salt);
//            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
//            return computedHash.SequenceEqual(hash);

//        }
//    }
//}