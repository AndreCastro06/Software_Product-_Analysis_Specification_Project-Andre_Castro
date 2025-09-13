using Microsoft.EntityFrameworkCore;
using PeaceAPI.Data;
using PeaceAPI.DTOs;
using PeaceAPI.Models;

namespace PeaceAPI.Services;

public interface IPacienteService
{
    Task<Paciente> CriarAsync(Guid nutricionistaId, CriarPacienteRequest req);
    Task<List<Paciente>> ListarPorNutricionistaAsync(Guid nutricionistaId);
}

public class PacienteService : IPacienteService
{
    private readonly PeaceDbContext _db;
    public PacienteService(PeaceDbContext db) => _db = db;

    public async Task<Paciente> CriarAsync(Guid nutricionistaId, CriarPacienteRequest req)
    {
        var p = new Paciente
        {
            NomeCompleto = req.NomeCompleto,
            DataNascimento = req.DataNascimento,
            Email = req.Email,
            Telefone = req.Telefone,
            NutricionistaId = nutricionistaId
        };
        _db.Pacientes.Add(p);
        await _db.SaveChangesAsync();
        return p;
    }

    public Task<List<Paciente>> ListarPorNutricionistaAsync(Guid nutricionistaId)
        => _db.Pacientes
              .Where(x => x.NutricionistaId == nutricionistaId)
              .OrderByDescending(x => x.CriadoEm)
              .ToListAsync();
}