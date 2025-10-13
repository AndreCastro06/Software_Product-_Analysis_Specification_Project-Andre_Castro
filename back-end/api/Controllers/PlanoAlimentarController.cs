using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PEACE.api.DTOs;
using PEACE.api.Services;

namespace PEACE.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanoAlimentarController : ControllerBase
    {
        private readonly PlanoAlimentarService _service;

        public PlanoAlimentarController(PlanoAlimentarService service)
        {
            _service = service;
        }

        // Criar plano alimentar
        [HttpPost]
        [Authorize(Roles = "Nutricionista")]
        public async Task<ActionResult> Criar([FromBody] CriarPlanoAlimentarDTO dto)
        {
            var plano = await _service.CriarAsync(dto);
            return CreatedAtAction(nameof(ObterDetalhado), new { planoId = plano.Id }, plano);
        }

        // Obter plano alimentar detalhado
        [HttpGet("{planoId}")]
        [Authorize(Roles = "Nutricionista")]
        public async Task<ActionResult<PlanoAlimentarDetalhadoDTO>> ObterDetalhado(int planoId)
        {
            var dto = await _service.ObterDetalhadoAsync(planoId);
            if (dto == null) return NotFound();

            return Ok(dto);
        }

        //  Exportar plano alimentar em PDF
        [HttpGet("{planoId}/exportar")]
        [Authorize(Roles = "Nutricionista")]
        public async Task<IActionResult> ExportarPdf(int planoId)
        {
            var (pdfBytes, nomeArquivo) = await _service.ExportarPdfAsync(planoId);
            if (pdfBytes == null || pdfBytes.Length == 0) return NotFound("PDF não pôde ser gerado.");
            return File(pdfBytes, "application/pdf", nomeArquivo);
        }
    }
}