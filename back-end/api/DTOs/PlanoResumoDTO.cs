namespace PEACE.api.DTOs
{ 
public class PlanoResumoDTO
{
    public int Id { get; set; }
    public string NomePaciente { get; set; }
    public DateTime DataCriacao { get; set; }

    public string DataFormatada => DataCriacao.ToString("dd/MM/yyyy");

}
}