
using Microsoft.AspNetCore.Mvc;
using PEACE.api.DTOs;
using PEACE.api.Services;

namespace PEACE.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly NutricionistaAuthService _nutricionistaAuthService;


        public AuthController(NutricionistaAuthService nutricionistaAuthService)
        {
            _nutricionistaAuthService = nutricionistaAuthService;

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

            // Se não encontrar em nenhum dos dois
            return Unauthorized("Email ou senha inválidos.");

        }
    }
}
