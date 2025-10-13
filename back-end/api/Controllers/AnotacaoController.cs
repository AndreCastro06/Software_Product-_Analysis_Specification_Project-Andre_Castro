using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using PEACE.api.Data;
using PEACE.api.DTOs;
using PEACE.api.Models;

namespace PEACE.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnotacaoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AnotacaoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPatch("{id}/anotacao")]
        [Authorize(Roles = "Paciente")]
        public async Task<IActionResult> AtualizarAnotacao(int id, [FromBody] AtualizarAnotacaoDTO dto)
        {
            var refeicao = await _context.Refeicoes
                .Include(r => r.HistoricoAnotacoes)
                .FirstOrDefaultAsync(r => r.Id == id);
            if (refeicao == null) return NotFound();

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (refeicao.PacienteId != userId) return Forbid();

            if (!string.IsNullOrWhiteSpace(refeicao.Anotacao))
            {
                _context.AnotacoesRefeicaoHistorico.Add(new AnotacaoRefeicaoHistorico
                {
                    RefeicaoId = refeicao.Id,
                    TextoAnterior = refeicao.Anotacao!,
                    EditadoEm = DateTime.UtcNow
                });
            }

            refeicao.Anotacao = dto.Anotacao;
            refeicao.AnotacaoEditadaEm = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return NoContent();
        }

   
        [HttpGet("{id}/historico-anotacoes")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<AnotacaoHistoricoDTO>>> GetHistoricoAnotacoes(int id)
        {
            var refeicao = await _context.Refeicoes
                .Include(r => r.HistoricoAnotacoes)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (refeicao == null) return NotFound();

            var historicoFormatado = refeicao.HistoricoAnotacoes
                .OrderBy(h => h.EditadoEm)
                .Select(h => new AnotacaoHistoricoDTO
                {
                    Texto = h.TextoAnterior,
                    EditadoEm = h.EditadoEm
                })
                .ToList();

            return historicoFormatado;

        }
    }
}