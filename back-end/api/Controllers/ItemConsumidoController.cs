using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PEACE.api.Data;
using PEACE.api.Models;

namespace PEACE.api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ItemConsumidoController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ItemConsumidoController(AppDbContext context) => _context = context;

        [HttpGet("{refeicaoId}")]
        public async Task<ActionResult<IEnumerable<ItemConsumido>>> GetByRefeicao(int refeicaoId)
        {
            return await _context.ItensConsumidos
                .Where(i => i.RefeicaoId == refeicaoId)
                .Include(i => i.Alimento)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<ItemConsumido>> Create(ItemConsumido item)
        {
            _context.ItensConsumidos.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetByRefeicao), new { refeicaoId = item.RefeicaoId }, item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.ItensConsumidos.FindAsync(id);
            if (item is null) return NotFound();
            _context.ItensConsumidos.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}