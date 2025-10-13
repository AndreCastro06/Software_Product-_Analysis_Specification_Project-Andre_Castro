
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PEACE.api.DTOs;
using PEACE.api.Models;
using PEACE.api.Services;

namespace PEACE.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvaliacaoAntropometricaController : ControllerBase
    {
        private readonly AvaliacaoAntropometricaService _service;

        public AvaliacaoAntropometricaController(AvaliacaoAntropometricaService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize(Roles = "Nutricionista")]
        public async Task<ActionResult<AvaliacaoAntropometrica>> Criar([FromBody] AvaliacaoAntropometricaDTO dto)
        {
            var avaliacao = await _service.CriarAsync(dto);
            return Ok(avaliacao);
        }

        [HttpGet("paciente/{pacienteId}")]
        [Authorize(Roles = "Nutricionista")]
        public async Task<ActionResult<IEnumerable<AvaliacaoAntropometrica>>> ListarPorPaciente(int pacienteId)
        {
            var avaliacoes = await _service.ListarPorPacienteAsync(pacienteId);
            return Ok(avaliacoes);
        }

        [HttpPost("simular")]
        [Authorize(Roles = "Nutricionista")]
        public ActionResult<ResultadoAvaliacaoDTO> Simular([FromBody] AvaliacaoAntropometricaDTO dto)
        {
            var resultado = _service.CalcularResultado(dto);
            return Ok(resultado);

        }
    }
}