using PEACE.api.DTOs;

namespace PEACE.api.DTOs
{
    public class RefeicaoPlanoDetalhadoDTO
    {
        public string Nome { get; set; } = string.Empty;
        public TimeSpan? Horario { get; set; }
        public List<ItemPlanoDetalhadoDTO> Itens { get; set; } = new();
        public ResumoMacrosDTO Totais { get; set; } = new();

    }
}
