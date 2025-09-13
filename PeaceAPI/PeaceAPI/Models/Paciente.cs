namespace PeaceAPI.Models;

public class Paciente
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string NomeCompleto { get; set; } = default!;
    public DateOnly DataNascimento { get; set; }
    public string? Email { get; set; }
    public string? Telefone { get; set; }
    public Guid NutricionistaId { get; set; }   // FK para Usuario
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
}