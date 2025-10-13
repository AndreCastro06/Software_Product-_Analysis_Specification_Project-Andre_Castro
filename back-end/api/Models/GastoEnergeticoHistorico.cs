using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PEACE.api.Enums;

namespace PEACE.api.Models
{
    public class GastoEnergeticoHistorico
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PacienteId { get; set; }

        [ForeignKey("PacienteId")]
        public Paciente? Paciente { get; set; }

        [Required]
        public ProtocoloGEB Protocolo { get; set; }

        [Required]
        public Sexo Sexo { get; set; }

        [Required]
        public FatorAtividade FatorAtividade { get; set; }

        [Required]
        public double Peso { get; set; }

        [Required]
        public double Altura { get; set; }

        [Required]
        public int Idade { get; set; }

        [Required]
        public double GEB { get; set; }

        [Required]
        public double GET { get; set; }
        public DateTime DataCalculo { get; set; } = DateTime.UtcNow;
    }
}