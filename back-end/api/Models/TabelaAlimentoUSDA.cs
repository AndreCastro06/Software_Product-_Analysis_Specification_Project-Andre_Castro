namespace PEACE.api.Models
{
    public class TabelaAlimentoUSDA
    {
        public int Id { get; set; }
        public string DescricaoIngles { get; set; } = string.Empty;
        public string? DescricaoTraduzida { get; set; }
        public double QuantidadePadrao { get; set; } = 100;
        public string UnidadeMedida { get; set; } = "g";
        public ICollection<NutrienteUSDA> Nutrientes { get; set; } = new List<NutrienteUSDA>();

    }
}
