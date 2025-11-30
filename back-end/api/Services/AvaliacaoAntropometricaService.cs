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
            // 1. Obter anamnese
            var anamnese = await _context.Anamneses
                .FirstOrDefaultAsync(a => a.PacienteId == dto.PacienteId);

            if (anamnese == null)
                throw new Exception("Anamnese não encontrada para este paciente.");

            // 2. Extrair dados da anamnese
            var sexo = anamnese.Sexo;
            var peso = anamnese.Peso;
            var altura = anamnese.Altura;
            var idade = anamnese.Idade;

            // BLINDAGEM: peso/altura inválidos
            if (peso <= 0 || altura <= 0)
                throw new Exception($"Dados antropométricos inválidos na anamnese: peso={peso}, altura={altura}");

            // Fator de atividade: usa enum → double real
            double fatorAtividade = anamnese.FatorAtividade switch
            {
                FatorAtividade.Sedentario => 1.20,
                FatorAtividade.LevementeAtivo => 1.375,
                FatorAtividade.ModeradamenteAtivo => 1.55,
                FatorAtividade.MuitoAtivo => 1.725,
                FatorAtividade.ExtremamenteAtivo => 1.90,
                _ => 1.20
            };

            // 3. Calcular resultado de composição
            var resultado = CalcularResultado(dto, sexo, peso, altura, idade);

            // 4. IMC seguro
            var denominadorImc = Math.Pow(altura / 100, 2);
            double imc = 0;

            if (denominadorImc > 0)
                imc = Math.Round(peso / denominadorImc, 2);
            else
                throw new Exception("Altura inválida para cálculo de IMC.");

            if (double.IsNaN(imc) || double.IsInfinity(imc))
                throw new Exception($"IMC inválido calculado: {imc}");

            // 5. Criar avaliação
            var avaliacao = new AvaliacaoAntropometrica
            {
                PacienteId = dto.PacienteId,

                Sexo = sexo,
                Idade = idade,
                Peso = peso,
                Altura = altura,

                CircunferenciaCintura = dto.CircunferenciaCintura,
                CircunferenciaQuadril = dto.CircunferenciaQuadril,

                TMB = resultado.TMB,
                GEB = resultado.TMB,
                GET = Math.Round(resultado.TMB * fatorAtividade, 2),
                FatorAtividade = fatorAtividade,

                Metodo = dto.Metodo,
                PercentualGordura = resultado.PercentualGordura,
                MassaGorda = resultado.MassaGorda,
                MassaMagra = resultado.MassaMagra,
                IMC = imc,

                DataAvaliacao = dto.DataAvaliacao == default
                ? DateTime.UtcNow
                : dto.DataAvaliacao
            };

            _context.AvaliacoesAntropometricas.Add(avaliacao);
            await _context.SaveChangesAsync();

            // 6. Salvar pregas
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

        //public async Task<List<AvaliacaoAntropometrica>> ListarPorPacienteAsync(int pacienteId)
        //{
        //    return await _context.AvaliacoesAntropometricas
        //        .Include(a => a.PregasCutaneas)
        //        .Where(a => a.PacienteId == pacienteId)
        //        .OrderByDescending(a => a.DataAvaliacao)
        //        .ToListAsync();
        //}

        public async Task<List<AvaliacaoHistoricoDTO>> ListarPorPacienteAsync(int pacienteId)
        {
            return await _context.AvaliacoesAntropometricas
                .Where(a => a.PacienteId == pacienteId)
                .OrderByDescending(a => a.DataAvaliacao)
                .Select(a => new AvaliacaoHistoricoDTO
                {
                    Id = a.Id,
                    PercentualGordura = a.PercentualGordura,
                    MassaGorda = a.MassaGorda,
                    MassaMagra = a.MassaMagra,
                    Peso = a.Peso,
                    Idade = a.Idade,
                    Metodo = a.Metodo,
                    DataAvaliacao = a.DataAvaliacao
                })
                .ToListAsync();
        }


        private ResultadoAvaliacaoDTO CalcularResultado(
            AvaliacaoAntropometricaDTO dto,
            Sexo sexo,
            double peso,
            double altura,
            int idade)
        {
            double somaDobras = 0;
            double percentualGordura = 0;
            double densidade = 0;

            bool masculino = sexo == Sexo.Masculino;

            switch (dto.Metodo)
            {
                case MetodoAvaliacao.Faulkner:
                    somaDobras = (dto.PCT ?? 0) + (dto.PCSA ?? 0) + (dto.PCSI ?? 0) + (dto.PCAB ?? 0);
                    percentualGordura = (somaDobras * 0.153) + 5.783;
                    break;

                case MetodoAvaliacao.JacksonPollock3Dobras:
                    if (masculino)
                    {
                        somaDobras = (dto.PCB ?? 0) + (dto.PCAB ?? 0) + (dto.PCCX ?? 0);
                        densidade = 1.10938
                            - (0.0008267 * somaDobras)
                            + (0.0000016 * Math.Pow(somaDobras, 2))
                            - (0.0002574 * idade);
                    }
                    else
                    {
                        somaDobras = (dto.PCT ?? 0) + (dto.PCSI ?? 0) + (dto.PCCX ?? 0);
                        densidade = 1.0994921
                            - (0.0009929 * somaDobras)
                            + (0.0000023 * Math.Pow(somaDobras, 2))
                            - (0.0001392 * idade);
                    }

                    break;

                case MetodoAvaliacao.JacksonPollock7Dobras:
                    somaDobras = (dto.PCT ?? 0) + (dto.PCSA ?? 0) + (dto.PCB ?? 0)
                               + (dto.PCAB ?? 0) + (dto.PCSI ?? 0) + (dto.PCSE ?? 0) + (dto.PCCX ?? 0);

                    densidade = masculino
                        ? 1.112 - (0.00043499 * somaDobras) + (0.00000055 * Math.Pow(somaDobras, 2)) - (0.00028826 * idade)
                        : 1.097 - (0.00042041 * somaDobras) + (0.00000058 * Math.Pow(somaDobras, 2)) - (0.0002166 * idade);
                    break;

                case MetodoAvaliacao.DurninWomersley:
                    somaDobras = (dto.PCT ?? 0) + (dto.PCB ?? 0) + (dto.PCSA ?? 0) + (dto.PCSI ?? 0);

                    double logSoma = Math.Log10(Math.Max(1, somaDobras));

                    densidade = masculino
                        ? 1.1631 - (0.0632 * logSoma)
                        : 1.1599 - (0.0717 * logSoma);
                    break;

                case MetodoAvaliacao.Guedes:
                    somaDobras = (dto.PCT ?? 0) + (dto.PCSA ?? 0) + (dto.PCSI ?? 0) + (dto.PCP ?? 0);
                    percentualGordura = masculino
                        ? (0.61 * somaDobras) - (0.16 * idade) + 3.8
                        : (0.55 * somaDobras) - (0.14 * idade) + 5.8;
                    break;

                default:
                    throw new Exception("Método de avaliação inválido.");
            }

            // Se o método depende de densidade, calcula % de gordura aqui
            if (dto.Metodo is MetodoAvaliacao.JacksonPollock3Dobras
                or MetodoAvaliacao.JacksonPollock7Dobras
                or MetodoAvaliacao.DurninWomersley)
            {
                if (densidade <= 0 || double.IsNaN(densidade) || double.IsInfinity(densidade))
                    throw new Exception($"Densidade corporal inválida calculada: {densidade}");

                percentualGordura = ((4.95 / densidade) - 4.5) * 100;
            }

            if (percentualGordura <= 0 || double.IsNaN(percentualGordura) || double.IsInfinity(percentualGordura))
                throw new Exception($"Percentual de gordura inválido calculado: {percentualGordura}");

            double massaGorda = (peso * percentualGordura) / 100;
            double massaMagra = peso - massaGorda;

            double tmb = masculino
                ? (10 * peso) + (6.25 * altura) - (5 * idade) + 5
                : (10 * peso) + (6.25 * altura) - (5 * idade) - 161;

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
