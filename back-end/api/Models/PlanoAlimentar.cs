using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PEACE.api.Models
{
    public class PlanoAlimentar
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PacienteId { get; set; }

        [ForeignKey("PacienteId")]
        public Paciente? Paciente { get; set; }

        public string Objetivo { get; set; } = string.Empty; // Ex: "Emagrecimento", "Hipertrofia"

        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        public ICollection<RefeicaoPlano> Refeicoes { get; set; } = new List<RefeicaoPlano>();
    }

    public class RefeicaoPlano
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; } = string.Empty;

        public TimeSpan? Horario { get; set; }

        [Required]
        public int PlanoAlimentarId { get; set; }

        [ForeignKey("PlanoAlimentarId")]
        public PlanoAlimentar? PlanoAlimentar { get; set; }

        public ICollection<ItemPlano> Itens { get; set; } = new List<ItemPlano>();
    }

    public class ItemPlano
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AlimentoId { get; set; }

        [ForeignKey("AlimentoId")]
        public Alimento? Alimento { get; set; }

        public double QuantidadeGramas { get; set; }

        [Required]
        public int RefeicaoPlanoId { get; set; }

        [ForeignKey("RefeicaoPlanoId")]
        public RefeicaoPlano? Refeicao { get; set; }

    }
}


