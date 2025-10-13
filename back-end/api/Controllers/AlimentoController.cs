using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PEACE.api.Data;
using PEACE.api.DTOs;
using PEACE.api.Models;

namespace PEACE.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlimentoController : ControllerBase
    {
        private readonly AppDbContext _context;
        public AlimentoController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlimentoReadDTO>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var alimentos = await _context.Alimentos
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(a => new AlimentoReadDTO
                {
                    Id = a.Id,
                    Descricao = a.Descricao,
                    Energia = a.Energia,
                    Carboidrato = a.Carboidrato,
                    Proteina = a.Proteina,
                    Lipidio = a.Lipidio
                })
                .ToListAsync();

            return Ok(alimentos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AlimentoReadDTO>> Get(int id)
        {
            var alimento = await _context.Alimentos.FindAsync(id);
            if (alimento is null) return NotFound();

            return Ok(new AlimentoReadDTO
            {
                Id = alimento.Id,
                Descricao = alimento.Descricao,
                Energia = alimento.Energia,
                Carboidrato = alimento.Carboidrato,
                Proteina = alimento.Proteina,
                Lipidio = alimento.Lipidio
            });
        }

        [HttpPost]
        [Authorize(Roles = "Nutricionista")]
        public async Task<ActionResult<Alimento>> Create(Alimento alimento)
        {
            _context.Alimentos.Add(alimento);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = alimento.Id }, alimento);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Nutricionista")]
        public async Task<IActionResult> Update(int id, Alimento alimento)
        {
            if (id != alimento.Id) return BadRequest();

            _context.Entry(alimento).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Nutricionista")]
        public async Task<IActionResult> Delete(int id)
        {
            var alimento = await _context.Alimentos.FindAsync(id);
            if (alimento is null) return NotFound();

            _context.Alimentos.Remove(alimento);
            await _context.SaveChangesAsync();

            return NoContent();

        }
    }
}
