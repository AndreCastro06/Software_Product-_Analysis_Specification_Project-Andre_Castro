using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PEACE.api.Models

{
    public class TabelaTaco
    {
        [Key]
        public int NumeroAlimento { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public int Id { get; set; }
        public double? Umidade { get; set; }
        public double? EnergiaKcal { get; set; }
        public double? EnergiaKj { get; set; }
        public double? Proteina { get; set; }
        public double? Lipideos { get; set; }
        public double? Colesterol { get; set; }
        public double? Carboidrato { get; set; }
        public double? FibraAlimentar { get; set; }
        public double? Cinzas { get; set; }

      
        

        // Minerais
        public double? Calcio { get; set; }
        public double? Magnesio { get; set; }
        public double? Manganes { get; set; }
        public double? Fosforo { get; set; }
        public double? Ferro { get; set; }
        public double? Sodio { get; set; }
        public double? Potassio { get; set; }
        public double? Cobre { get; set; }
        public double? Zinco { get; set; }

        // Vitaminas
        public double? Retinol { get; set; }
        public double? RE { get; set; }
        public double? RAE { get; set; }
        public double? Tiamina { get; set; }
        public double? Riboflavina { get; set; }
        public double? Piridoxina { get; set; }
        public double? Niacina { get; set; }
        public double? VitaminaC { get; set; }

        public AcidosGraxosTaco? AcidosGraxos { get; set; }
        public AminoacidosTaco? Aminoacidos { get; set; }

    }
}
