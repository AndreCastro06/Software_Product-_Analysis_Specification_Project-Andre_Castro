using System.ComponentModel.DataAnnotations;
using PEACE.api.Models;

public class Alimento
{
    public int Id { get; set; }

    [Required]
    public string Descricao { get; set; } = string.Empty;

    [Required]
    public FonteDeDados Fonte { get; set; }

    public double QuantidadeReferencia { get; set; }
    public string UnidadeMedida { get; set; } = "g";

    // 🔧 Propriedades esperadas pelos DTOs/controllers:
    public double Energia { get; set; }
    public double Carboidrato { get; set; }
    public double Proteina { get; set; }
    public double Lipidio { get; set; }

    public ICollection<Nutriente>? Nutrientes { get; set; }

}