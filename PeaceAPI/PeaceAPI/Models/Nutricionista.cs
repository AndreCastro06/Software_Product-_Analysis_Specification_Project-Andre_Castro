namespace PeaceAPI.Models;

public class Nutricionista

{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string NomeCompleto { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string CRN { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public string Role { get; set; } = "Nutricionista";
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
}