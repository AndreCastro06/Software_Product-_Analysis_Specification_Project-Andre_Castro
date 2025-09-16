using System.ComponentModel.DataAnnotations;

namespace PeaceApi.DTOs
{
    public class RegisterNutricionistaDTO
    {
        [Required, StringLength(120)]
        public string NomeCompleto { get; set; } = default!;

        [Required, EmailAddress, StringLength(180)]
        public string Email { get; set; } = default!;

        // 12345 ou 12345A
        [Required]
        [RegularExpression(@"^\d{5}([A-Za-z])?$",
            ErrorMessage = "CRN deve ter 5 dígitos e, opcionalmente, 1 letra no final (ex.: 12345 ou 12345A).")]
        public string CRN { get; set; } = default!;

        [Required, StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = default!;
    }


    public class RefreshTokenRequestDTO
    {
        [Required]
        public string RefreshToken { get; set; } = default!;
    }

    public class LogoutRequestDTO
    {
        [Required]
        public string RefreshToken { get; set; } = default!;
    }

    public class NutricionistaResponseDTO
    {
        public Guid Id { get; set; }
        public string NomeCompleto { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string CRN { get; set; } = default!;
        public bool Ativo { get; set; }
        public bool Deletado { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? AtualizadoEm { get; set; }
        public DateTime? DeletadoEm { get; set; }
    }

    public class NutricionistaListItemDTO
    {
        public Guid Id { get; set; }
        public string NomeCompleto { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string CRN { get; set; } = default!;
        public bool Ativo { get; set; }
        public bool Deletado { get; set; }
    }

    public class CreateNutricionistaDTO
    {
        [Required, StringLength(120)]
        public string NomeCompleto { get; set; } = default!;

        [Required, EmailAddress, StringLength(180)]
        public string Email { get; set; } = default!;

        // 12345 ou 12345A
        [Required]
        [RegularExpression(@"^\d{5}([A-Za-z])?$",
            ErrorMessage = "CRN deve ter 5 dígitos e, opcionalmente, 1 letra no final (ex.: 12345 ou 12345A).")]
        public string CRN { get; set; } = default!;

        [Required, StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = default!;

        public bool Ativo { get; set; } = true;
    }
    public class UpdateNutricionistaDTO
    {
        [Required, StringLength(120)]
        public string NomeCompleto { get; set; } = default!;

        // 12345 ou 12345A
        [Required]
        [RegularExpression(@"^\d{5}([A-Za-z])?$",
            ErrorMessage = "CRN deve ter 5 dígitos e, opcionalmente, 1 letra no final (ex.: 12345 ou 12345A).")]
        public string CRN { get; set; } = default!;

        public bool? Ativo { get; set; }
    }

public class ChangePasswordDTO
    {
        // Opcional para fluxo "eu mesmo altero"
        [StringLength(100, MinimumLength = 6)]
        public string? SenhaAtual { get; set; }

        [Required, StringLength(100, MinimumLength = 6)]
        public string NovaSenha { get; set; } = default!;

        [Required, Compare(nameof(NovaSenha), ErrorMessage = "As senhas não coincidem.")]
        public string ConfirmacaoNovaSenha { get; set; } = default!;
    }

    public class PagedResultDTO<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
    }

    public class AuthResultDTO
    {
        public string AccessToken { get; set; } = default!;
        public DateTime ExpiresAt { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiresAt { get; set; }
        public NutricionistaResponseDTO Usuario { get; set; } = default!;

        // No seu domínio, quem administra é o próprio Nutricionista
        public string Role { get; set; } = "Nutricionista";
    }
}