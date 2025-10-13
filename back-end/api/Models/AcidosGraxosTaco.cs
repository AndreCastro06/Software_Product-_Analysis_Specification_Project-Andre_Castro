using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PEACE.api.Models
{
    public class AcidosGraxosTaco
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NumeroAlimento { get; set; }

        [ForeignKey(nameof(NumeroAlimento))]
        public TabelaTaco TabelaTaco { get; set; } = null!;

        // Adicione aqui as propriedades dos ácidos graxos
        public double? Saturados { get; set; }
        public double? Monoinsaturados { get; set; }
        public double? Poliinsaturados { get; set; }
        public double? DozeZero { get; set; }
        public double? QuatorzeZero { get; set; }
        public double? DezesseisZero { get; set; }
        public double? DezoitoZero { get; set; }
        public double? VinteZero { get; set; }
        public double? VinteDoisZero { get; set; }
        public double? VinteQuatroZero { get; set; }
        public double? QuatorzeUm { get; set; }
        public double? DezesseisUm { get; set; }
        public double? DezoitoUm { get; set; }
        public double? VinteUm { get; set; }
        public double? DezoitoDoisN6 { get; set; }
        public double? DezoitoTresN3 { get; set; }
        public double? VinteQuatroCinco { get; set; }
        public double? VinteCincoCinco { get; set; }
        public double? VinteDoisCinco { get; set; }
        public double? VinteDoisSeis { get; set; }
        public double? DezoitoUmT { get; set; }
        public double? DezoitoDoisT { get; set; }

    }
}
