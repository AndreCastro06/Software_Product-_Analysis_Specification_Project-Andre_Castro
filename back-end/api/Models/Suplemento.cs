using System;
using PEACE.api.Models;

public class Suplemento
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Marca { get; set; }
    public string Posologia { get; set; } // Ex: "1 cápsula após o almoço"
    public string Horario { get; set; } // Ex: "12:00", ou criar um Enum
    public string Finalidade { get; set; } // Ex: "Ganho de massa", "Melhora do sono"

    public int? PacienteId { get; set; }
    public Paciente Paciente { get; set; }

    public int? PlanoAlimentarId { get; set; }
    public PlanoAlimentar PlanoAlimentar { get; set; }

}