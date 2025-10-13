namespace PEACE.api.Models
{
    public class ItemConsumido
    {
        public int Id { get; set; }
        public double QuantidadeGramas { get; set; }
        public int RefeicaoId { get; set; }
        public Refeicao? Refeicao { get; set; }
        public int AlimentoId { get; set; }
        public Alimento? Alimento { get; set; }
        public double Quantidade { get; set; }


    }
}
