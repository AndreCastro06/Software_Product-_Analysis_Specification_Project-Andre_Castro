using PEACE.api.Models;

public class Refeicao
{
    public int Id { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public DateTime DataHora { get; set; }

    // 🔧 Adicionar:
    public string? Anotacao { get; set; }
    public DateTime? AnotacaoEditadaEm { get; set; }

    public ICollection<ItemConsumido> ItensConsumidos { get; set; } = new List<ItemConsumido>();
    public ICollection<AnotacaoRefeicaoHistorico> HistoricoAnotacoes { get; set; } = new List<AnotacaoRefeicaoHistorico>();
    public int PacienteId { get; set; }
    public Paciente? Paciente { get; set; }
}
