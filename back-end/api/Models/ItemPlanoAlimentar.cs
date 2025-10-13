using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PEACE.api.Models
{
    public class ItemPlanoAlimentar
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int RefeicaoPlanoId { get; set; }

        [ForeignKey("RefeicaoPlanoId")]
        public RefeicaoPlano? RefeicaoPlano { get; set; }

        [Required]
        public int AlimentoId { get; set; }

        [ForeignKey("AlimentoId")]
        public Alimento? Alimento { get; set; }

        [Required]
        public double QuantidadeGramas { get; set; }

        // Cálculo automático (poderá ser preenchido no back)
        public double Calorias { get; set; }
        public double Proteinas { get; set; }
        public double Carboidratos { get; set; }
        public double Gorduras { get; set; }

    }
}