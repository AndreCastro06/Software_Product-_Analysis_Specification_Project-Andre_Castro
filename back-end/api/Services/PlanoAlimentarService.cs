using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PEACE.api.Data;
using PEACE.api.DTOs;
using PEACE.api.Models;
using System.Text;
using System.Globalization;
using System.IO;

using System.Globalization;

namespace PEACE.api.Services
{
    public class PlanoAlimentarService
    {
        private readonly AppDbContext _context;

        public PlanoAlimentarService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PlanoAlimentar> CriarAsync(CriarPlanoAlimentarDTO dto)
        {
            var plano = new PlanoAlimentar
            {
                PacienteId = dto.PacienteId,
                Objetivo = dto.Objetivo,
                DataCriacao = DateTime.UtcNow,
                Refeicoes = new List<RefeicaoPlano>()
            };

            foreach (var refeicaoDto in dto.Refeicoes)
            {
                var refeicao = new RefeicaoPlano
                {
                    Nome = refeicaoDto.Nome,
                    Horario = refeicaoDto.Horario,
                    Itens = new List<ItemPlano>()
                };

                foreach (var itemDto in refeicaoDto.Itens)
                {
                    var alimento = await _context.TabelaTaco.FindAsync(itemDto.AlimentoId);
                    if (alimento == null) continue;

                    refeicao.Itens.Add(new ItemPlano
                    {
                        AlimentoId = itemDto.AlimentoId,
                        QuantidadeGramas = itemDto.QuantidadeGramas
                    });
                }

                plano.Refeicoes.Add(refeicao);
            }

            _context.PlanosAlimentares.Add(plano);
            await _context.SaveChangesAsync();

            return plano;
        }
        public async Task<PlanoAlimentarDetalhadoDTO?> ObterDetalhadoAsync(int planoId)
        {
            var plano = await _context.PlanosAlimentares
                .Include(p => p.Paciente)
                .Include(p => p.Refeicoes)
                    .ThenInclude(r => r.Itens)
                    .ThenInclude(i => i.Alimento)
                .FirstOrDefaultAsync(p => p.Id == planoId);

            if (plano == null) return null;

            var dto = new PlanoAlimentarDetalhadoDTO
            {
                NomePaciente = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(plano.Paciente?.NomeCompleto?.Trim().ToLower() ?? "Paciente"),
                NomeNutricionista = plano.Paciente?.Nutricionista?.NomeCompleto ?? "Nutricionista",
                DataCriacao = plano.DataCriacao,
                Refeicoes = new List<RefeicaoPlanoDetalhadoDTO>(),
                TotaisDoDia = new ResumoMacrosDTO()
            };

            foreach (var refeicao in plano.Refeicoes)
            {
                var refDto = new RefeicaoPlanoDetalhadoDTO
                {
                    Nome = refeicao.Nome,
                    Horario = refeicao.Horario,
                    Itens = new List<ItemPlanoDetalhadoDTO>(),
                    Totais = new ResumoMacrosDTO()
                };

                foreach (var item in refeicao.Itens)
                {
                    var tacoAlimento = await _context.TabelaTaco.FindAsync(item.AlimentoId);
                    var macros = CalcularMacros(tacoAlimento, item.QuantidadeGramas);

                    refDto.Totais.Calorias += macros.Calorias;
                    refDto.Totais.Proteinas += macros.Proteinas;
                    refDto.Totais.Carboidratos += macros.Carboidratos;
                    refDto.Totais.Gorduras += macros.Gorduras;

                    refDto.Itens.Add(new ItemPlanoDetalhadoDTO
                    {
                        AlimentoId = item.AlimentoId,
                        QuantidadeGramas = item.QuantidadeGramas,
                        NomeAlimento = tacoAlimento?.Descricao ?? "Desconhecido",
                        Macros = macros
                    });
                }

                dto.TotaisDoDia.Calorias += refDto.Totais.Calorias;
                dto.TotaisDoDia.Proteinas += refDto.Totais.Proteinas;
                dto.TotaisDoDia.Carboidratos += refDto.Totais.Carboidratos;
                dto.TotaisDoDia.Gorduras += refDto.Totais.Gorduras;

                dto.Refeicoes.Add(refDto);
            }

            return dto;
        }

        private ResumoMacrosDTO CalcularMacros(TabelaTaco? alimento, double gramas)
        {
            if (alimento == null) return new();

            return new ResumoMacrosDTO
            {
                Calorias = Math.Round(((alimento.EnergiaKcal ?? 0) * gramas) / 100, 2),
                Proteinas = Math.Round(((alimento.Proteina ?? 0) * gramas) / 100, 2),
                Carboidratos = Math.Round(((alimento.Carboidrato ?? 0) * gramas) / 100, 2),
                Gorduras = Math.Round(((alimento.Lipideos ?? 0) * gramas) / 100, 2)
            };
        }

        public async Task<(byte[] pdfBytes, string nomeArquivo)> ExportarPdfAsync(int planoId)
        {
            var dto = await ObterDetalhadoAsync(planoId);
            if (dto == null) return (Array.Empty<byte>(), "");

            var sb = new StringBuilder();

            sb.AppendLine($"Plano Alimentar - {dto.NomePaciente}");
            sb.AppendLine($"Nutricionista: {dto.NomeNutricionista}");
            sb.AppendLine($"Data de Criação: {dto.DataCriacao.ToString("dd/MM/yyyy")}");
            sb.AppendLine();

            foreach (var refeicao in dto.Refeicoes)
            {
                sb.AppendLine($"🍽️ {refeicao.Nome} - {refeicao.Horario?.ToString(@"hh\:mm") ?? "Sem horário definido"}");

                foreach (var item in refeicao.Itens)
                {
                    sb.AppendLine($"• {item.NomeAlimento} - {item.QuantidadeGramas}g");
                    sb.AppendLine($"  Kcal: {item.Macros.Calorias} | Proteína: {item.Macros.Proteinas}g | Carbs: {item.Macros.Carboidratos}g | Gordura: {item.Macros.Gorduras}g");
                }

                sb.AppendLine($"➕ Totais da refeição: {refeicao.Totais.Calorias} Kcal | {refeicao.Totais.Proteinas}g Proteínas");
                sb.AppendLine();
            }

            sb.AppendLine("📊 Totais do Dia:");
            sb.AppendLine($"Kcal: {dto.TotaisDoDia.Calorias}");
            sb.AppendLine($"Proteínas: {dto.TotaisDoDia.Proteinas}g");
            sb.AppendLine($"Carboidratos: {dto.TotaisDoDia.Carboidratos}g");
            sb.AppendLine($"Gorduras: {dto.TotaisDoDia.Gorduras}g");


            sb.AppendLine();
            sb.AppendLine($"Nutricionista Responsável: {dto.NomeNutricionista}");
            sb.AppendLine($"CRN: {dto.CRN ?? "Não informado"}");
            sb.AppendLine($"E-mail: {dto.EmailNutricionista ?? "Não informado"}");

            // Criar o PDF com PdfSharpCore.
            var conteudo = sb.ToString();
            var bytes = Encoding.UTF8.GetBytes(conteudo);

            var nomeArquivo = $"Plano Alimentar - {dto.NomePaciente} - PEACE - Nutricionista {dto.NomeNutricionista}.pdf";

            return (bytes, nomeArquivo);
        }

    }
}
