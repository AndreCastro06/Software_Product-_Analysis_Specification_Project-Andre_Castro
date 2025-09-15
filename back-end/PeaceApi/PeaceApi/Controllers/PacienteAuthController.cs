using Microsoft.AspNetCore.Mvc;
using PeaceApi.DTOs;
using PeaceApi.Services;

namespace PeaceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacienteAuthController : ControllerBase
    {
        private readonly PacienteAuthService _authService;

        public PacienteAuthController(PacienteAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterPacienteDTO dto)
        {
            var result = await _authService.RegisterAsync(dto);

            if (result == null)
                return BadRequest("Email já cadastrado.");

            return Ok(new
            {
                result.NomeCompleto,
                result.Email
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