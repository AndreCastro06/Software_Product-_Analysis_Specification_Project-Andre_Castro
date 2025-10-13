using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PEACE.api.Data; 
using PEACE.api.Models; 
using PEACE.api.DTOs;


namespace PEACE.api.Services
{
    public class SuplementoService
    {
        private readonly AppDbContext _context;

        public SuplementoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SuplementoResponseDTO> CriarAsync(SuplementoRequestDTO dto)
        {
            var suplemento = new Suplemento
            {
                Nome = dto.Nome,
                Marca = dto.Marca,
                Posologia = dto.Posologia,
                Horario = dto.Horario,
                Finalidade = dto.Finalidade,
                PacienteId = dto.PacienteId,
                PlanoAlimentarId = dto.PlanoAlimentarId
            };

            _context.Suplementos.Add(suplemento);
            await _context.SaveChangesAsync();

            return new SuplementoResponseDTO
            {
                Id = suplemento.Id,
                Nome = suplemento.Nome,
                Marca = suplemento.Marca,
                Posologia = suplemento.Posologia,
                Horario = suplemento.Horario,
                Finalidade = suplemento.Finalidade,
                PacienteId = suplemento.PacienteId,
                PlanoAlimentarId = suplemento.PlanoAlimentarId
            };
        }

        public async Task<List<SuplementoResponseDTO>> ListarPorPacienteAsync(int pacienteId)
        {
            return await _context.Suplementos
                .Where(s => s.PacienteId == pacienteId)
                .Select(s => new SuplementoResponseDTO
                {
                    Id = s.Id,
                    Nome = s.Nome,
                    Marca = s.Marca,
                    Posologia = s.Posologia,
                    Horario = s.Horario,
                    Finalidade = s.Finalidade,
                    PacienteId = s.PacienteId,
                    PlanoAlimentarId = s.PlanoAlimentarId
                })
                .ToListAsync();
        }

        public async Task EditarAsync(int id, SuplementoRequestDTO dto)
        {
            var suplemento = await _context.Suplementos.FindAsync(id);
            if (suplemento == null) throw new Exception("Suplemento não encontrado.");

            suplemento.Nome = dto.Nome;
            suplemento.Marca = dto.Marca;
            suplemento.Posologia = dto.Posologia;
            suplemento.Horario = dto.Horario;
            suplemento.Finalidade = dto.Finalidade;

            await _context.SaveChangesAsync();
        }

        public async Task ExcluirAsync(int id)
        {
            var suplemento = await _context.Suplementos.FindAsync(id);
            if (suplemento == null) throw new Exception("Suplemento não encontrado.");

            _context.Suplementos.Remove(suplemento);
            await _context.SaveChangesAsync();
        }

    }
}