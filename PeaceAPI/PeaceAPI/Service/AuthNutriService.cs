using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PeaceAPI.Data;
using PeaceAPI.DTOs;
using PeaceAPI.Models;

namespace PeaceAPI.Services;

public class AuthNutriService : IAuthNutriService
{
    private readonly PeaceDbContext _db;
    private readonly PasswordHasher<Nutricionista> _hasher = new();

    public AuthNutriService(PeaceDbContext db) => _db = db;

    public async Task<Nutricionista> RegistrarAsync(RegisterNutricionistaRequest req)
    {
        if (await _db.Nutricionistas.AnyAsync(u => u.Email == req.Email))
            throw new InvalidOperationException("E-mail já cadastrado.");

        var user = new Nutricionista
        {
            NomeCompleto = req.NomeCompleto,
            Email = req.Email,
            CRN = req.CRN,
            Role = "Nutricionista"
        };

        user.PasswordHash = _hasher.HashPassword(user, req.Password);
        _db.Nutricionistas.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }

    public async Task<Nutricionista?> ValidarLoginAsync(LoginRequest req)
    {
        var user = await _db.Nutricionistas.FirstOrDefaultAsync(u => u.Email == req.Email);
        if (user is null) return null;

        var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, req.Password);
        return result == PasswordVerificationResult.Success ? user : null;
    }
}