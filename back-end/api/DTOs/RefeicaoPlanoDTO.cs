using PEACE.api.DTOs;

public class RefeicaoPlanoDTO
{
    public string Nome { get; set; } = string.Empty;
    public TimeSpan? Horario { get; set; }
    public List<ItemPlanoAlimentarDTO> Itens { get; set; } = new();

}