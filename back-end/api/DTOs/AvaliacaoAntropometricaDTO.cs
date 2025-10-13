using PEACE.api.Enums;
using PEACE.api.Models;

namespace PEACE.api.DTOs
{
    public class AvaliacaoAntropometricaDTO
    {
        public int PacienteId { get; set; }

        // Dados básicos
        public string Sexo { get; set; } = string.Empty;
        public int Idade { get; set; }
        public double Peso { get; set; }
        public bool PesoEmLibras { get; set; } = false;
        public double Altura { get; set; }

        // Circunferências
        public double CircunferenciaCintura { get; set; }
        public double CircunferenciaQuadril { get; set; }

        // Dados energéticos
        public double GEB { get; set; }
        public double GET { get; set; }
        public FatorAtividade FatorAtividade { get; set; }

        // Composição corporal
        public MetodoAvaliacao Metodo { get; set; }
        public double PercentualGordura { get; set; }
        public double MassaGorda { get; set; }
        public double MassaMagra { get; set; }
        public double TMB { get; set; }

        // Pregas cutâneas (input para cálculos)
        public double? PCB { get; set; }
        public double? PCP { get; set; }
        public double? PCT { get; set; }
        public double? PCSA { get; set; }
        public double? PCSE { get; set; }
        public double? PCSI { get; set; }
        public double? PCAB { get; set; }
        public double? PCCX { get; set; }

    }
}
