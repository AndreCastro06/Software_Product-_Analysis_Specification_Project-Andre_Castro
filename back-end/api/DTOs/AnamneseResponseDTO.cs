using System;
using PEACE.api.Enums;
using PeaceApi.Enums;

namespace PEACE.api.DTOs
{
    public class AnamneseResponseDTO
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public string NomeCompleto { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }

        // Calculado no backend
        public int Idade { get; set; }

        public string? Ocupacao { get; set; }

        // Dados físicos básicos
        public string Sexo { get; set; } = string.Empty;
        public double Peso { get; set; }
        public double Altura { get; set; }
        public FatorAtividade FatorAtividade { get; set; }

        // Atividade física
        public bool PraticaAtividadeFisica { get; set; }
        public string? AtividadeFisicaTipo { get; set; }
        public string? AtividadeFisicaHorario { get; set; }
        public string? AtividadeFisicaFrequencia { get; set; }

        // Histórico familiar
        public bool HistoricoFamiliar_HAS { get; set; }
        public bool HistoricoFamiliar_DM { get; set; }
        public bool HistoricoFamiliar_Hipercolesterolemia { get; set; }
        public bool HistoricoFamiliar_DoencaCardiaca { get; set; }

        // Histórico pessoal
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

        // Hábitos
        public double? ConsumoAguaDiario { get; set; }
        public FrequenciaIntestinal FrequenciaIntestinal { get; set; }
    }
}
