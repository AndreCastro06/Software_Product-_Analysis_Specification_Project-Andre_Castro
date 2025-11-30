using PEACE.api.Models;

namespace PEACE.api.DTOs
{
    public class AvaliacaoAntropometricaDTO
    {
        public int PacienteId { get; set; }

        // Circunferências
        public double CircunferenciaCintura { get; set; }
        public double CircunferenciaQuadril { get; set; }

        // Pregas cutâneas
        public double? PCB { get; set; }  // Bíceps
        public double? PCP { get; set; }  // Peito
        public double? PCT { get; set; }  // Tríceps
        public double? PCSA { get; set; } // Supra-axilar
        public double? PCSE { get; set; } // Subescapular
        public double? PCSI { get; set; } // Supra-ilíaca
        public double? PCAB { get; set; } // Abdominal
        public double? PCCX { get; set; } // Coxa

        public MetodoAvaliacao Metodo { get; set; }
        public DateTime DataAvaliacao { get; set; }
    }
}