using PEACE.api.Models;

public class Paciente
{
    public int Id { get; set; }
    public int? NutricionistaId { get; set; }
    public Nutricionista? Nutricionista { get; set; }
    public required string NomeCompleto { get; set; }


    public DateTime DataNascimento { get; set; }
    public byte[]? PasswordHash { get; set; }
    public byte[]? PasswordSalt { get; set; }
    public ICollection<Refeicao> Refeicoes { get; set; } = new List<Refeicao>();

    public Anamnese? Anamnese { get; set; }


}