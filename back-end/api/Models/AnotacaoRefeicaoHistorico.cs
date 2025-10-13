using System;

namespace PEACE.api.Models
{
    public class AnotacaoRefeicaoHistorico
    {
        public int Id { get; set; }
        public int RefeicaoId { get; set; }
        public string TextoAnterior { get; set; } = string.Empty;
        public DateTime EditadoEm { get; set; } = DateTime.UtcNow;
        public Refeicao? Refeicao { get; set; }

    }
}