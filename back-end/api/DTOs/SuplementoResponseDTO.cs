using System;

namespace PEACE.api.DTOs
{
    public class SuplementoResponseDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Marca { get; set; }
        public string Posologia { get; set; }
        public string Horario { get; set; }
        public string Finalidade { get; set; }
        public int? PacienteId { get; set; }
        public int? PlanoAlimentarId { get; set; }

    }
}