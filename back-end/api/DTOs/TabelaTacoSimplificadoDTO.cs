// DTOs/TabelaTacoSimplificadoDTO.cs

namespace PEACE.api.DTOs
{
    public class TabelaTacoSimplificadoDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public double EnergiaKcal { get; set; }
        public double Proteina { get; set; }
        public double Lipideos { get; set; }
        public double Carboidrato { get; set; }

    }
}
