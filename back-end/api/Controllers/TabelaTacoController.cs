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
    public class TabelaTacoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TabelaTacoController(AppDbContext context)
        {
            _context = context;
        }

        // Endpoint completo (visível para Nutricionista)
        [HttpGet("detalhado")]
        [Authorize(Roles = "Nutricionista")]
        public async Task<ActionResult<IEnumerable<TabelaTaco>>> GetDetalhado()
        {
            var alimentos = await _context.TabelaTaco
                .Include(t => t.AcidosGraxos)
                .Include(t => t.Aminoacidos)
                .ToListAsync();
            return alimentos;
        }

        // Endpoint simplificado (para Paciente ou uso leigo)
        [HttpGet("basico")]
        [Authorize(Roles = "Paciente,Nutricionista")]
        public async Task<ActionResult<IEnumerable<TabelaTacoSimplificadoDTO>>> GetBasico()
        {
            var alimentos = await _context.TabelaTaco
                .Select(t => new TabelaTacoSimplificadoDTO
                {
                    Id = t.Id,
                    Descricao = t.Descricao,
                    EnergiaKcal = t.EnergiaKcal ?? 0,
                    Proteina = t.Proteina ?? 0,
                    Lipideos = t.Lipideos ?? 0,
                    Carboidrato = t.Carboidrato ?? 0
                })
                .ToListAsync();
            return alimentos;
        }
    }
}
