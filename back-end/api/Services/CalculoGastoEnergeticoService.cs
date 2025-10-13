using PEACE.api.DTOs;
using PEACE.api.Enums;

namespace PEACE.api.Services
{
    public class CalculoGastoEnergeticoService
    {
        public ResultadoCalculoGETDTO CalcularGEBEGET(CalculoGEBDTO dto)
        {
            double geb = dto.Protocolo switch
            {
                ProtocoloGEB.HarrisBenedict => dto.Sexo == Sexo.Masculino
                    ? 66.47 + (13.75 * dto.Peso) + (5.0 * dto.Altura) - (6.76 * dto.Idade)
                    : 655.1 + (9.56 * dto.Peso) + (1.85 * dto.Altura) - (4.68 * dto.Idade),

                ProtocoloGEB.MifflinStJeor => dto.Sexo == Sexo.Masculino
                    ? (10 * dto.Peso) + (6.25 * dto.Altura) - (5 * dto.Idade) + 5
                    : (10 * dto.Peso) + (6.25 * dto.Altura) - (5 * dto.Idade) - 161,

                _ => throw new ArgumentException("Protocolo inválido.")
            };

            double fator = (double)dto.FatorAtividade / 100;
            double get = geb * fator;

            return new ResultadoCalculoGETDTO
            {
                GEB = Math.Round(geb, 2),
                GET = Math.Round(get, 2),
                Protocolo = dto.Protocolo.ToString()
            };
        }
    }
}