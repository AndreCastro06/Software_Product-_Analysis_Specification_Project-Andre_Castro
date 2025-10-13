using Microsoft.EntityFrameworkCore;
using PEACE.api.DTOs;
using PEACE.api.Models;
using PEACE.api.Data;
using PEACE.api.Enums;

namespace PEACE.api.Services
{
    public class AvaliacaoAntropometricaService
    {
        private readonly AppDbContext _context;

        public AvaliacaoAntropometricaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AvaliacaoAntropometrica> CriarAsync(AvaliacaoAntropometricaDTO dto)
        {
            double peso = dto.PesoEmLibras ? dto.Peso * 0.453592 : dto.Peso;

            var resultado = CalcularResultado(dto);
            var imc = Math.Round(peso / Math.Pow(dto.Altura / 100, 2), 2);

            var avaliacao = new AvaliacaoAntropometrica
            {
                PacienteId = dto.PacienteId,
                Sexo = Enum.Parse<Sexo>(dto.Sexo, true),
                Idade = dto.Idade,
                Peso = peso,
                PesoEmLibras = dto.PesoEmLibras,
                Altura = dto.Altura,
                CircunferenciaCintura = dto.CircunferenciaCintura,
                CircunferenciaQuadril = dto.CircunferenciaQuadril,
                GEB = resultado.TMB,
                GET = resultado.TMB * ((double)dto.FatorAtividade / 100),
                FatorAtividade = dto.FatorAtividade,
                Metodo = dto.Metodo,
                PercentualGordura = resultado.PercentualGordura,
                MassaGorda = resultado.MassaGorda,
                MassaMagra = resultado.MassaMagra,
                TMB = resultado.TMB,
                IMC = imc,
                DataAvaliacao = DateTime.UtcNow
            };

            _context.AvaliacoesAntropometricas.Add(avaliacao);
            await _context.SaveChangesAsync();

            // Salvar as pregas
            var pregas = new PregasCutaneas
            {
                AvaliacaoAntropometricaId = avaliacao.Id,
                PCB = dto.PCB,
                PCT = dto.PCT,
                PCSA = dto.PCSA,
                PCSE = dto.PCSE,
                PCSI = dto.PCSI,
                PCAB = dto.PCAB,
                PCCX = dto.PCCX,
                PCP = dto.PCP
            };

            _context.PregasCutaneas.Add(pregas);
            await _context.SaveChangesAsync();

            return avaliacao;
        }

        public async Task<List<AvaliacaoAntropometrica>> ListarPorPacienteAsync(int pacienteId)
        {
            return await _context.AvaliacoesAntropometricas
                .Include(a => a.PregasCutaneas)
                .Where(a => a.PacienteId == pacienteId)
                .OrderByDescending(a => a.DataAvaliacao)
                .ToListAsync();
        }

        public ResultadoAvaliacaoDTO CalcularResultado(AvaliacaoAntropometricaDTO dto)
        {
            double peso = dto.PesoEmLibras ? dto.Peso * 0.453592 : dto.Peso;
            double percentualGordura = 0;
            double somaDobras = 0;
            double densidade = 0;

            switch (dto.Metodo)
            {
                case MetodoAvaliacao.Faulkner:
                    somaDobras = (dto.PCT ?? 0) + (dto.PCSA ?? 0) + (dto.PCSI ?? 0) + (dto.PCAB ?? 0);
                    percentualGordura = (somaDobras * 0.153) + 5.783;
                    break;

                case MetodoAvaliacao.JacksonPollock3Dobras:
                    if (dto.Sexo.ToLower() == "masculino")
                    {
                        somaDobras = (dto.PCB ?? 0) + (dto.PCAB ?? 0) + (dto.PCCX ?? 0);
                        densidade = 1.10938 - (0.0008267 * somaDobras) + (0.0000016 * Math.Pow(somaDobras, 2)) - (0.0002574 * dto.Idade);
                    }
                    else
                    {
                        somaDobras = (dto.PCT ?? 0) + (dto.PCSI ?? 0) + (dto.PCCX ?? 0);
                        densidade = 1.0994921 - (0.0009929 * somaDobras) + (0.0000023 * Math.Pow(somaDobras, 2)) - (0.0001392 * dto.Idade);
                    }
                    percentualGordura = ((4.95 / densidade) - 4.5) * 100;
                    break;

                case MetodoAvaliacao.JacksonPollock7Dobras:
                    somaDobras = (dto.PCT ?? 0) + (dto.PCSA ?? 0) + (dto.PCB ?? 0) + (dto.PCAB ?? 0)
                               + (dto.PCSI ?? 0) + (dto.PCSE ?? 0) + (dto.PCCX ?? 0);
                    densidade = dto.Sexo.ToLower() == "masculino"
                        ? 1.112 - (0.00043499 * somaDobras) + (0.00000055 * Math.Pow(somaDobras, 2)) - (0.00028826 * dto.Idade)
                        : 1.097 - (0.00042041 * somaDobras) + (0.00000058 * Math.Pow(somaDobras, 2)) - (0.0002166 * dto.Idade);
                    percentualGordura = ((4.95 / densidade) - 4.5) * 100;
                    break;

                case MetodoAvaliacao.DurninWomersley:
                    somaDobras = (dto.PCT ?? 0) + (dto.PCB ?? 0) + (dto.PCSA ?? 0) + (dto.PCSI ?? 0);
                    double logSoma = Math.Log10(somaDobras);
                    densidade = dto.Sexo.ToLower() == "masculino"
                        ? 1.1631 - (0.0632 * logSoma)
                        : 1.1599 - (0.0717 * logSoma);
                    percentualGordura = ((4.95 / densidade) - 4.5) * 100;
                    break;

                case MetodoAvaliacao.Guedes:
                    somaDobras = (dto.PCT ?? 0) + (dto.PCSA ?? 0) + (dto.PCSI ?? 0) + (dto.PCP ?? 0);
                    percentualGordura = dto.Sexo.ToLower() == "masculino"
                        ? (0.61 * somaDobras) - (0.16 * dto.Idade) + 3.8
                        : (0.55 * somaDobras) - (0.14 * dto.Idade) + 5.8;
                    break;

                default:
                    throw new ArgumentException("Método de avaliação inválido.");
            }

            double massaGorda = (peso * percentualGordura) / 100;
            double massaMagra = peso - massaGorda;

            double tmb = dto.Sexo.ToLower() == "masculino"
                ? (10 * peso) + (6.25 * dto.Altura) - (5 * dto.Idade) + 5
                : (10 * peso) + (6.25 * dto.Altura) - (5 * dto.Idade) - 161;

            return new ResultadoAvaliacaoDTO
            {
                PercentualGordura = Math.Round(percentualGordura, 2),
                MassaGorda = Math.Round(massaGorda, 2),
                MassaMagra = Math.Round(massaMagra, 2),
                TMB = Math.Round(tmb, 2)
            };
        }
    }
}