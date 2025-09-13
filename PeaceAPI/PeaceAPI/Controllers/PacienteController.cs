using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeaceAPI.DTOs;
using PeaceAPI.Services;

namespace PeaceAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Nutricionista")]
public class PacientesController : ControllerBase
{
    private readonly IPacienteService _svc;
    public PacientesController(IPacienteService svc) => _svc = svc;

    private Guid GetUserId()
    {
        var sub = User.FindFirstValue("sub") ?? User.FindFirstValue(ClaimTypes.NameIdentifier);
        return Guid.Parse(sub!);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarPacienteRequest req)
    {
        var id = GetUserId();
        var p = await _svc.CriarAsync(id, req);
        var resp = new PacienteResponse(
            p.Id, p.NomeCompleto, PacienteResponse.CalcularIdade(p.DataNascimento),
            p.Email, p.Telefone, p.CriadoEm);
        return CreatedAtAction(nameof(Criar), new { id = p.Id }, resp);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PacienteResponse>>> Listar()
    {
        var id = GetUserId();
        var lista = await _svc.ListarPorNutricionistaAsync(id);
        return lista.Select(p => new PacienteResponse(
            p.Id, p.NomeCompleto, PacienteResponse.CalcularIdade(p.DataNascimento),
            p.Email, p.Telefone, p.CriadoEm)).ToList();
    }
}