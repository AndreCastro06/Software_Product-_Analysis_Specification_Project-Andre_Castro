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

        // ===========================
        //  POST - Criar Avaliação
        // ===========================
        [HttpPost]
        [Authorize(Roles = "Nutricionista")]
        public async Task<ActionResult<AvaliacaoResultadoDTO>> Criar([FromBody] AvaliacaoAntropometricaDTO dto)
        {
            try
            {
                var avaliacao = await _service.CriarAsync(dto);

                return Ok(new AvaliacaoResultadoDTO
                {
                    PercentualGordura = avaliacao.PercentualGordura,
                    MassaGorda = avaliacao.MassaGorda,
                    MassaMagra = avaliacao.MassaMagra,
                    Metodo = avaliacao.Metodo,
                    Peso = avaliacao.Peso,
                    Idade = avaliacao.Idade
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }
        // ===========================
        //  GET - Listar por Paciente
        // ===========================
        [HttpGet("paciente/{pacienteId}")]
        public async Task<ActionResult<IEnumerable<AvaliacaoHistoricoDTO>>> Listar(int pacienteId)
        {
            try
            {
                var avaliacoes = await _service.ListarPorPacienteAsync(pacienteId);
                return Ok(avaliacoes);
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }
    }
}

