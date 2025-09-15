using PeaceApi.Models;

public class Paciente
{
    public int Id { get; set; }
    public int? NutricionistaId { get; set; }
    public Nutricionista? Nutricionista { get; set; }
    public required string NomeCompleto { get; set; }
    public string Email { get; set; } = string.Empty;

    public DateTime DataNascimento { get; set; }
    public byte[]? PasswordHash { get; set; }
    public byte[]? PasswordSalt { get; set; }
    
}