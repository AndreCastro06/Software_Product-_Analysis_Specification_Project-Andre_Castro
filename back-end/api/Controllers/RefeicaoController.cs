using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PEACE.api.Data;
using PEACE.api.Models;
using System.Security.Claims;

namespace PEACE.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RefeicaoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RefeicaoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Refeicao>>> GetRefeicoes()
        {
            return await _context.Refeicoes.Include(r => r.ItensConsumidos).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Refeicao>> GetRefeicao(int id)
        {
            var refeicao = await _context.Refeicoes
                .Include(r => r.ItensConsumidos)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (refeicao == null) return NotFound();
            return refeicao;
        }

        [HttpPost]
        public async Task<ActionResult<Refeicao>> CreateRefeicao(Refeicao refeicao)
        {
            _context.Refeicoes.Add(refeicao);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRefeicao), new { id = refeicao.Id }, refeicao);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRefeicao(int id, Refeicao refeicao)
        {
            if (id != refeicao.Id) return BadRequest();

            _context.Entry(refeicao).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRefeicao(int id)
        {
            var refeicao = await _context.Refeicoes.FindAsync(id);
            if (refeicao == null) return NotFound();

            _context.Refeicoes.Remove(refeicao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id}/anotacao")]
        [Authorize(Roles = "Paciente")]
        public async Task<IActionResult> AtualizarAnotacao(int id, [FromBody] string anotacao)
        {
            var refeicao = await _context.Refeicoes.FindAsync(id);
            if (refeicao == null) return NotFound();

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (refeicao.PacienteId != userId) return Forbid();

            refeicao.Anotacao = anotacao;
            _context.Refeicoes.Update(refeicao);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
