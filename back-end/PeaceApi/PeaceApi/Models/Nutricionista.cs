namespace PeaceApi.Models
{
    public class Nutricionista
    {
        public int Id { get; set; }
        public required string NomeCompleto { get; set; }
        public required string Email { get; set; }
        public required string CRN { get; set; }
        public byte[]? PasswordHash { get; set; }

        public byte[]? PasswordSalt { get; set; }
        public ICollection<Paciente> Pacientes { get; set; } = new List<Paciente>();


    }
}