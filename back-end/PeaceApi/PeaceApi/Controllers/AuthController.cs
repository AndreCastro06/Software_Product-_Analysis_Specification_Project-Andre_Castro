
using Microsoft.AspNetCore.Mvc;
using PeaceApi.DTOs;
using PeaceApi.Services;

namespace PeaceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly NutricionistaAuthService _nutricionistaAuthService;
        private readonly PacienteAuthService _pacienteAuthService;

        public AuthController(NutricionistaAuthService nutricionistaAuthService, PacienteAuthService pacienteAuthService)
        {
            _nutricionistaAuthService = nutricionistaAuthService;
            _pacienteAuthService = pacienteAuthService;
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            // Tentar login  como nutricionista primeiro
            var resultNutricionista = await _nutricionistaAuthService.LoginAsync(dto);

            if (resultNutricionista != null)
            {
                return Ok(resultNutricionista); // Login bem-sucedido como Nutricionista
            }

            // Se não encontrar, tenta como Paciente
            var resultPaciente = await _pacienteAuthService.LoginAsync(dto);

            if (resultPaciente != null)
            {
                return Ok(resultPaciente); // Login bem-sucedido como Paciente
            }

            // Se não encontrar em nenhum dos dois
            return Unauthorized("Email ou senha inválidos.");
        }
    }
}
