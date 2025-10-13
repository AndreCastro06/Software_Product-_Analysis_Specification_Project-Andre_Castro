namespace PEACE.api.DTOs
{
    public class PlanoAlimentarDetalhadoDTO
    {
        public string NomePaciente { get; set; } = string.Empty;
        public string NomeNutricionista { get; set; } = string.Empty;

        public string EmailNutricionista { get; set; } = string.Empty;

        public string CRN { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        public List<RefeicaoPlanoDetalhadoDTO> Refeicoes { get; set; } = new();

        public ResumoMacrosDTO TotaisDoDia { get; set; } = new();

    }
}