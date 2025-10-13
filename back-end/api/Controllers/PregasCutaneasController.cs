using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PEACE.api.Data;
using PEACE.api.Models;

namespace PEACE.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PregasCutaneasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PregasCutaneasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("avaliacao/{avaliacaoId}")]
        [Authorize(Roles = "Nutricionista")]
        public async Task<ActionResult<PregasCutaneas>> GetPorAvaliacao(int avaliacaoId)
        {
            var pregas = await _context.Set<PregasCutaneas>()
                .FirstOrDefaultAsync(p => p.AvaliacaoAntropometricaId == avaliacaoId);

            if (pregas == null) return NotFound();

            return pregas;
        }

        [HttpPost]
        [Authorize(Roles = "Nutricionista")]
        public async Task<ActionResult<PregasCutaneas>> Criar(PregasCutaneas pregas)
        {
            _context.Set<PregasCutaneas>().Add(pregas);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPorAvaliacao), new { avaliacaoId = pregas.AvaliacaoAntropometricaId }, pregas);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Nutricionista")]
        public async Task<IActionResult> Atualizar(int id, PregasCutaneas atualizada)
        {
            if (id != atualizada.Id) return BadRequest();

            _context.Entry(atualizada).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();

        }
    }
}
