using Microsoft.EntityFrameworkCore;
using PEACE.api.Data;
using PEACE.api.DTOs;
using PEACE.api.Models;

namespace PEACE.api.Services
{
    public class AnamneseService
    {
        private readonly AppDbContext _context;

        public AnamneseService(AppDbContext context)
        {
            _context = context;
        }

        //  Este é o método que você escreveu
        public async Task<AnamneseResponseDTO> CriarAsync(AnamneseRequestDTO dto)
        {
            var entity = new Anamnese
            {
                PacienteId = dto.PacienteId ?? 0,
                NomeCompleto = dto.NomeCompleto,
                DataNascimento = dto.DataNascimento,
                Ocupacao = dto.Ocupacao,
                PraticaAtividadeFisica = dto.PraticaAtividadeFisica,
                AtividadeFisicaTipo = dto.AtividadeFisicaTipo,
                AtividadeFisicaHorario = dto.AtividadeFisicaHorario,
                AtividadeFisicaFrequencia = dto.AtividadeFisicaFrequencia,
                HistoricoFamiliar_HAS = dto.HistoricoFamiliar_HAS,
                HistoricoFamiliar_DM = dto.HistoricoFamiliar_DM,
                HistoricoFamiliar_Hipercolesterolemia = dto.HistoricoFamiliar_Hipercolesterolemia,
                HistoricoFamiliar_DoencaCardiaca = dto.HistoricoFamiliar_DoencaCardiaca,
                HistoricoPessoal_HAS = dto.HistoricoPessoal_HAS,
                HistoricoPessoal_DM = dto.HistoricoPessoal_DM,
                HistoricoPessoal_Hipercolesterolemia = dto.HistoricoPessoal_Hipercolesterolemia,
                HistoricoPessoal_DoencaCardiaca = dto.HistoricoPessoal_DoencaCardiaca,
                UsaMedicamento = dto.UsaMedicamento,
                Medicamentos = dto.Medicamentos,
                UsaSuplemento = dto.UsaSuplemento,
                Suplementos = dto.Suplementos,
                TemAlergiaAlimentar = dto.TemAlergiaAlimentar,
                Alergias = dto.Alergias,
                IntoleranciaLactose = dto.IntoleranciaLactose,
                AversoesAlimentares = dto.AversoesAlimentares,
                ConsumoAguaDiario = dto.ConsumoAguaDiario,
                FrequenciaIntestinal = dto.FrequenciaIntestinal
            };

            _context.Anamneses.Add(entity);
            await _context.SaveChangesAsync();

            // Monta o retorno completo
            return new AnamneseResponseDTO
            {
                PacienteId = entity.PacienteId,
                NomeCompleto = entity.NomeCompleto,
                DataNascimento = entity.DataNascimento,
                Ocupacao = entity.Ocupacao,
                PraticaAtividadeFisica = entity.PraticaAtividadeFisica,
                AtividadeFisicaTipo = entity.AtividadeFisicaTipo,
                AtividadeFisicaHorario = entity.AtividadeFisicaHorario,
                AtividadeFisicaFrequencia = entity.AtividadeFisicaFrequencia,
                HistoricoFamiliar_HAS = entity.HistoricoFamiliar_HAS,
                HistoricoFamiliar_DM = entity.HistoricoFamiliar_DM,
                HistoricoFamiliar_Hipercolesterolemia = entity.HistoricoFamiliar_Hipercolesterolemia,
                HistoricoFamiliar_DoencaCardiaca = entity.HistoricoFamiliar_DoencaCardiaca,
                HistoricoPessoal_HAS = entity.HistoricoPessoal_HAS,
                HistoricoPessoal_DM = entity.HistoricoPessoal_DM,
                HistoricoPessoal_Hipercolesterolemia = entity.HistoricoPessoal_Hipercolesterolemia,
                HistoricoPessoal_DoencaCardiaca = entity.HistoricoPessoal_DoencaCardiaca,
                UsaMedicamento = entity.UsaMedicamento,
                Medicamentos = entity.Medicamentos,
                UsaSuplemento = entity.UsaSuplemento,
                Suplementos = entity.Suplementos,
                TemAlergiaAlimentar = entity.TemAlergiaAlimentar,
                Alergias = entity.Alergias,
                IntoleranciaLactose = entity.IntoleranciaLactose,
                AversoesAlimentares = entity.AversoesAlimentares,
                ConsumoAguaDiario = entity.ConsumoAguaDiario,
                FrequenciaIntestinal = entity.FrequenciaIntestinal,
                Sexo = dto.Sexo,
                Peso = dto.Peso,
                Altura = dto.Altura,
                FatorAtividade = dto.FatorAtividade
            };
        }
    }
}
