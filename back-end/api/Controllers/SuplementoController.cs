using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;
using PEACE.api.DTOs;
using PEACE.api.Services;

namespace PEACE.api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Nutricionista")]
    public class SuplementoController : ControllerBase
    {
        private readonly SuplementoService _service;

        public SuplementoController(SuplementoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<SuplementoResponseDTO>> Criar(SuplementoRequestDTO dto)
        {
            var result = await _service.CriarAsync(dto);
            return CreatedAtAction(nameof(ListarPorPaciente), new { pacienteId = result.PacienteId }, result);
        }

        [HttpGet("paciente/{pacienteId}")]
        public async Task<ActionResult<List<SuplementoResponseDTO>>> ListarPorPaciente(int pacienteId)
        {
            var lista = await _service.ListarPorPacienteAsync(pacienteId);
            return Ok(lista);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Editar(int id, SuplementoRequestDTO dto)
        {
            await _service.EditarAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            await _service.ExcluirAsync(id);
            return NoContent();

        }
    }
}