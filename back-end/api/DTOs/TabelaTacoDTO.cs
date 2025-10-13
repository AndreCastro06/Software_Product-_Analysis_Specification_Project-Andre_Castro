namespace PEACE.api.DTOs
{
    public class TabelaTacoReadDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public double Energia { get; set; }
        public double Proteina { get; set; }
        public double Lipideos { get; set; }
        public double Carboidratos { get; set; }
        public double Fibras { get; set; }
        public double Calcio { get; set; }
        public double Ferro { get; set; }
        public double Sodio { get; set; }
        public double Potassio { get; set; }
        public double Tiamina { get; set; }
        public double Riboflavina { get; set; }
        public double Niacina { get; set; }

        public AcidosGraxosDTO? AcidosGraxos { get; set; }
        public AminoacidosDTO? Aminoacidos { get; set; }
    }

    public class AcidosGraxosDTO
    {
        public double? AG12_0 { get; set; }
        public double? AG14_0 { get; set; }
        public double? AG16_0 { get; set; }
        public double? AG18_0 { get; set; }
        public double? AG18_1 { get; set; }
        public double? AG18_2 { get; set; }
        public double? AG18_3 { get; set; }
        public double? AG20_4 { get; set; }
        public double? AG22_6 { get; set; }
    }

    public class AminoacidosDTO
    {
        public double? Triptofano { get; set; }
        public double? Treonina { get; set; }
        public double? Isoleucina { get; set; }
        public double? Leucina { get; set; }
        public double? Lisina { get; set; }
        public double? Metionina { get; set; }
        public double? Fenilalanina { get; set; }
        public double? Tirosina { get; set; }
        public double? Valina { get; set; }

    }
}
