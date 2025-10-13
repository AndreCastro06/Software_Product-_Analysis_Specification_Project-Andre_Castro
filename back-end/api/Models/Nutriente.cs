using PEACE.api.Models;
using System.ComponentModel.DataAnnotations;

public class Nutriente
{
    public int Id { get; set; }

    [Required]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [RegularExpression(@"^[a-zA-Zµ]+$", ErrorMessage = "Unidade deve conter apenas letras (ex: g, mg, µg, UI, kcal).")]
    public string Unidade { get; set; } = string.Empty;

    [Range(0, double.MaxValue)]
    public double Valor { get; set; }

    public TipoNutriente Tipo { get; set; } = TipoNutriente.Outros;

    public SubtipoMicronutriente Subtipo { get; set; } = SubtipoMicronutriente.Nenhum;

    public int AlimentoId { get; set; }
    public Alimento? Alimento { get; set; }

}