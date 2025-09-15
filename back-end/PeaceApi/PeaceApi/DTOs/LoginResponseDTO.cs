namespace PeaceApi.DTOs
{
        public class LoginResponseDTO
        {
            public string Token { get; set; } = string.Empty;
            public string Nome { get; set; } = string.Empty;
            public string Role { get; set; } = string.Empty;
        }
}
