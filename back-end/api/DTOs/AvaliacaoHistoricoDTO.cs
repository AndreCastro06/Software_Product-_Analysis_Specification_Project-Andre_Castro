using PEACE.api.Models;

public class AvaliacaoHistoricoDTO
{
    public int Id { get; set; }
    public double PercentualGordura { get; set; }
    public double MassaGorda { get; set; }
    public double MassaMagra { get; set; }
    public double Peso { get; set; }
    public int Idade { get; set; }
    public MetodoAvaliacao Metodo { get; set; }
    public DateTime DataAvaliacao { get; set; }
}