using System;
using System.ComponentModel.DataAnnotations;
using PEACE.api.Enums;
using PeaceApi.Enums;

namespace PEACE.api.DTOs
{
    public class AnamneseRequestDTO
    {
        public int? PacienteId { get; set; }

        [Required]
        public string NomeCompleto { get; set; } = string.Empty;

        [Required]
        public DateTime DataNascimento { get; set; }

        public string? Ocupacao { get; set; }

        public bool PraticaAtividadeFisica { get; set; }
        public string? AtividadeFisicaTipo { get; set; }
        public string? AtividadeFisicaHorario { get; set; }
        public string? AtividadeFisicaFrequencia { get; set; }

        public bool HistoricoFamiliar_HAS { get; set; }
        public bool HistoricoFamiliar_DM { get; set; }
        public bool HistoricoFamiliar_Hipercolesterolemia { get; set; }
        public bool HistoricoFamiliar_DoencaCardiaca { get; set; }

        public bool HistoricoPessoal_HAS { get; set; }
        public bool HistoricoPessoal_DM { get; set; }
        public bool HistoricoPessoal_Hipercolesterolemia { get; set; }
        public bool HistoricoPessoal_DoencaCardiaca { get; set; }

        public bool UsaMedicamento { get; set; }
        public string? Medicamentos { get; set; }

        public bool UsaSuplemento { get; set; }
        public string? Suplementos { get; set; }

        public bool TemAlergiaAlimentar { get; set; }
        public string? Alergias { get; set; }

        public bool IntoleranciaLactose { get; set; }
        public string? AversoesAlimentares { get; set; }

        public double? ConsumoAguaDiario { get; set; }
        public FrequenciaIntestinal FrequenciaIntestinal { get; set; } = FrequenciaIntestinal.Desconhecida;

        // Campos que faltavam no DTO e que o Service usa:
        [Required]
        public Sexo Sexo { get; set; }

        [Range(10, 400)]
        public double Peso { get; set; }

        [Range(50, 250)]
        public double Altura { get; set; }

        public FatorAtividade FatorAtividade { get; set; } = FatorAtividade.Sedentario;
    }
}