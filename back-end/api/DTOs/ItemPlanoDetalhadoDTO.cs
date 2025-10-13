
namespace PEACE.api.DTOs
{
    public class ItemPlanoDetalhadoDTO : ItemPlanoAlimentarDTO
    {
        public string NomeAlimento { get; set; } = string.Empty;
        public ResumoMacrosDTO Macros { get; set; } = new();

    }
}