using Microsoft.AspNetCore.Mvc;
using PeaceAPI.DTOs;
using PeaceAPI.Services;

namespace PeaceAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _auth;
    private readonly ITokenService _token;

    public AuthController(IAuthService auth, ITokenService token)
    {
        _auth = auth;
        _token = token;
    }

    [HttpPost("registrar-nutri")]
    public async Task<IActionResult> RegisterNutri([FromBody] RegisterNutricionistaRequest req)
    {
        var user = await _auth.RegistrarNutricionistaAsync(req);
        return CreatedAtAction(nameof(RegisterNutri), new { id = user.Id }, new { user.Id, user.NomeCompleto, user.Email });
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest req)
    {
        var user = await _auth.ValidarLoginAsync(req);
        if (user is null) return Unauthorized(new { message = "Credenciais inválidas." });

        var jwt = _token.GerarToken(user);
        return new AuthResponse(jwt, user.NomeCompleto, user.Role);
    }
}