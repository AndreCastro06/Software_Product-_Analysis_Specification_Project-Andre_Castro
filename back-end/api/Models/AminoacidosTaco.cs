using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PEACE.api.Models
{
    public class AminoacidosTaco
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NumeroAlimento { get; set; }

        [ForeignKey(nameof(NumeroAlimento))]
        public TabelaTaco TabelaTaco { get; set; } = null!;

        // Adicione aqui os aminoácidos
        public double? Triptofano { get; set; }
        public double? Treonina { get; set; }
        public double? Isoleucina { get; set; }
        public double? Leucina { get; set; }
        public double? Lisina { get; set; }
        public double? Metionina { get; set; }
        public double? Cistina { get; set; }
        public double? Fenilalanina { get; set; }
        public double? Tirosina { get; set; }
        public double? Valina { get; set; }
        public double? Arginina { get; set; }
        public double? Histidina { get; set; }
        public double? Alanina { get; set; }
        public double? Aspartico { get; set; }
        public double? Glutamico { get; set; }
        public double? Glicina { get; set; }
        public double? Prolina { get; set; }
        public double? Serina { get; set; }

    }
}
