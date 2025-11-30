using System.ComponentModel.DataAnnotations;
using PEACE.api.Models;

namespace PEACE.api.Models
{
    public class PregasCutaneas
    {
        public int Id { get; set; }

        [Required]
        public int AvaliacaoAntropometricaId { get; set; }
        public AvaliacaoAntropometrica? AvaliacaoAntropometrica { get; set; }

        // Pregas cutâneas (em mm)
        public double? PCB { get; set; }   // Biciptal
        public double? PCT { get; set; }   // Tricipital
        public double? PCSA { get; set; }  // SubAxilar
        public double? PCSE { get; set; }  // Subescapular
        public double? PCSI { get; set; }  // Suprailíaca
        public double? PCAB { get; set; }  // Abdominal
        public double? PCP { get; set; }  // Panturilha
        public double? PCCX { get; set; }  // COXA
    }
}
