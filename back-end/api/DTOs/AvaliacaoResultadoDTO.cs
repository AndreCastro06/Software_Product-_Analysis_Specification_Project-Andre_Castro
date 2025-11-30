using PEACE.api.Models;

namespace PEACE.api.DTOs
{
    public class AvaliacaoResultadoDTO
    {
        public double PercentualGordura { get; set; }
        public double MassaGorda { get; set; }
        public double MassaMagra { get; set; }
        public MetodoAvaliacao Metodo { get; set; }
        public double Peso { get; set; }
        public int Idade { get; set; }
    }
}