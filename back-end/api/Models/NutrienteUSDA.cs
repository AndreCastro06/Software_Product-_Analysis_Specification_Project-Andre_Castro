namespace PEACE.api.Models
{
    public class NutrienteUSDA
    {
        public int Id { get; set; }
        public string NomeOriginal { get; set; } = string.Empty;   // Ex: "Protein", "Total Fat"
        public string NomePadronizado { get; set; } = string.Empty; // Ex: "Proteína", "Gorduras Totais"
        public double Valor { get; set; }
        public string Unidade { get; set; } = string.Empty;

        public SubtipoMicronutriente Subtipo { get; set; } = SubtipoMicronutriente.Nenhum;

        public int AlimentoUSDAId { get; set; }
        public TabelaAlimentoUSDA? AlimentoUSDA { get; set; }

    }
}
