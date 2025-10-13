using PEACE.api.Models;

namespace PEACE.api.DTOs;

public class PacienteDadosSuplementacaoDTO
{
    public int Id { get; set; }
    public string NomeCompleto { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Email { get; set; }

    public int Idade => CalcularIdade();

    private int CalcularIdade()
    {
        var hoje = DateTime.Today;
        var idade = hoje.Year - DataNascimento.Year;
        if (DataNascimento.Date > hoje.AddYears(-idade)) idade--;
        return idade;

    }
}