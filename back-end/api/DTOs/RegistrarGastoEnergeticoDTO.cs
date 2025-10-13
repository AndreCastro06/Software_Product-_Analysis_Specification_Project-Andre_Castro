using PEACE.api.Enums;

namespace PEACE.api.DTOs
{
    public class RegistrarGastoEnergeticoDTO
    {
        public int PacienteId { get; set; }
        public ProtocoloGEB Protocolo { get; set; }
        public Sexo Sexo { get; set; }
        public double Peso { get; set; }
        public double Altura { get; set; }
        public int Idade { get; set; }
        public FatorAtividade FatorAtividade { get; set; }

    }
}