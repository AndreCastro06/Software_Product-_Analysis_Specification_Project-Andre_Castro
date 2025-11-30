
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PEACE.api.Enums;

namespace PEACE.api.Models
{
    public class AvaliacaoAntropometrica
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PacienteId { get; set; }

        [Required]
        public DateTime DataAvaliacao { get; set; }

        [ForeignKey("PacienteId")]
        public Paciente? Paciente { get; set; }

        // Dados básicos
        public Sexo Sexo { get; set; }
        public int Idade { get; set; }
        public double Peso { get; set; }
        public bool PesoEmLibras { get; set; } = false;
        public double Altura { get; set; }

        public double IMC { get; set; }

        // Circunferências
        public double CircunferenciaCintura { get; set; }
        public double CircunferenciaQuadril { get; set; }

        // Dados energéticos
        public double GEB { get; set; }
        public double GET { get; set; }
        public double FatorAtividade { get; set; }


        // Composição corporal
        public MetodoAvaliacao Metodo { get; set; }
        public double PercentualGordura { get; set; }
        public double MassaGorda { get; set; }
        public double MassaMagra { get; set; }
        public double TMB { get; set; }

        // Relacionamento com pregas
        public PregasCutaneas? PregasCutaneas { get; set; }
    }
}
