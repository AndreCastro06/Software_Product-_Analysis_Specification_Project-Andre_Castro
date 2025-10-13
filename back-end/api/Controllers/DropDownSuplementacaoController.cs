using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PEACE.api.Data; 
using PEACE.api.DTOs;

namespace PEACE.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Nutricionista")]
    public class DropdownSuplementacaoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DropdownSuplementacaoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("pacientes")]
        public async Task<ActionResult<List<PacienteDadosSuplementacaoDTO>>> ListarPacientes()
        {
            var pacientes = await _context.Pacientes
                .Select(p => new PacienteDadosSuplementacaoDTO
                {
                    Id = p.Id,
                    NomeCompleto = p.NomeCompleto,
                    DataNascimento = p.DataNascimento,
       
                })
                .ToListAsync();

            return Ok(pacientes);
        }

        [HttpGet("planos-alimentares")]
        public async Task<ActionResult<List<PlanoResumoDTO>>> ListarPlanos()
        {
            var planos = await _context.PlanoAlimentar
                .Include(p => p.Paciente)
                .OrderByDescending(p => p.DataCriacao)
                .Select(p => new PlanoResumoDTO
                {
                    Id = p.Id,
                    NomePaciente = p.Paciente.NomeCompleto,
                    DataCriacao = p.DataCriacao
                })
                .ToListAsync();

            return Ok(planos);
        }

    }
}