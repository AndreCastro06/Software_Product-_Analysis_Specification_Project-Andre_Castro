namespace PEACE.api.DTOs
{
    public class AlimentoReadDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public double Energia { get; set; }
        public double Carboidrato { get; set; }
        public double Proteina { get; set; }
        public double Lipidio { get; set; }

    }
}
