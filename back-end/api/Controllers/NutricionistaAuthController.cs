using Microsoft.AspNetCore.Mvc;
using PEACE.api.DTOs;
using PEACE.api.Services;

namespace PEACE.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NutricionistaAuthController : ControllerBase
    {
        private readonly NutricionistaAuthService _authService;

        public NutricionistaAuthController(NutricionistaAuthService authService)
        {
            _authService = authService;
        }



        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterNutricionistaDTO dto)
        {
            var result = await _authService.RegisterAsync(dto);

            if (result == null)
                return BadRequest("Email já cadastrado.");

            return Ok(new
            {
                result.NomeCompleto,
                result.Email,
                result.CRN
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var result = await _authService.LoginAsync(dto);

            if (result == null)
                return Unauthorized("Email ou senha inválidos.");

            return Ok(result);

        }
    }
}