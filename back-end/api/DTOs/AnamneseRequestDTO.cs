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

        // Dado essencial para uso posterior (como Avaliação Física)
        [Required]
        public string Sexo { get; set; } = string.Empty;

        // Dados físicos básicos
        [Range(10, 400, ErrorMessage = "Peso deve estar entre 10kg e 400kg.")]
        public double Peso { get; set; }

        [Range(50, 250, ErrorMessage = "Altura deve estar entre 50cm e 250cm.")]
        public double Altura { get; set; }

        public FatorAtividade FatorAtividade { get; set; } = FatorAtividade.Sedentario;

        // Rotina e hábitos
        public bool PraticaAtividadeFisica { get; set; }
        public string? AtividadeFisicaTipo { get; set; }
        public string? AtividadeFisicaHorario { get; set; }
        public string? AtividadeFisicaFrequencia { get; set; }

        // Histórico familiar e pessoal
        public bool HistoricoFamiliar_HAS { get; set; }
        public bool HistoricoFamiliar_DM { get; set; }
        public bool HistoricoFamiliar_Hipercolesterolemia { get; set; }
        public bool HistoricoFamiliar_DoencaCardiaca { get; set; }

        public bool HistoricoPessoal_HAS { get; set; }
        public bool HistoricoPessoal_DM { get; set; }
        public bool HistoricoPessoal_Hipercolesterolemia { get; set; }
        public bool HistoricoPessoal_DoencaCardiaca { get; set; }

        // Medicações e suplementos
        public bool UsaMedicamento { get; set; }
        public string? Medicamentos { get; set; }
        public bool UsaSuplemento { get; set; }
        public string? Suplementos { get; set; }

        // Alergias e restrições
        public bool TemAlergiaAlimentar { get; set; }
        public string? Alergias { get; set; }
        public bool IntoleranciaLactose { get; set; }
        public string? AversoesAlimentares { get; set; }

        // Outros hábitos
        public double? ConsumoAguaDiario { get; set; }
        public FrequenciaIntestinal FrequenciaIntestinal { get; set; } = FrequenciaIntestinal.Desconhecida;
    }
}