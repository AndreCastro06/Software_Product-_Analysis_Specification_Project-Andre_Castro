
public class CriarPlanoAlimentarDTO
{
    public int PacienteId { get; set; }
    public string Objetivo { get; set; } = string.Empty;
    public List<RefeicaoPlanoDTO> Refeicoes { get; set; } = new();

}