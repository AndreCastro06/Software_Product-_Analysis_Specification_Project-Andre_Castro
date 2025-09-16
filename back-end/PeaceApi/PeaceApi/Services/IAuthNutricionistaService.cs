using System.Threading;
using System.Threading.Tasks;
using PeaceApi.DTOs;

namespace PeaceApi.Services
{
    /// <summary>
    /// Autenticação/Autorização do Nutricionista.
    /// Somente a role "Nutricionista" é utilizada no domínio.
    /// </summary>
    public interface INutricionistaAuthService
    {
        /// <summary>
        /// Registra um novo nutricionista (fluxo público).
        /// Deve validar CRN (5 dígitos, opcional + 1 letra), email único e
        /// aplicar hashing de senha antes de persistir.
        /// </summary>
        Task<NutricionistaResponseDTO?> RegisterAsync(RegisterNutricionistaDTO dto, CancellationToken ct);

        /// <summary>
        /// Autentica e retorna tokens (JWT + opcional refresh token) + dados do usuário.
        /// </summary>
        Task<AuthResultDTO?> LoginAsync(LoginDTO dto, CancellationToken ct);

        /// <summary>
        /// Gera um novo access token a partir do refresh token válido.
        /// </summary>
        Task<AuthResultDTO?> RefreshTokenAsync(RefreshTokenRequestDTO dto, CancellationToken ct);

        /// <summary>
        /// Invalida o refresh token atual (logout).
        /// </summary>
        Task LogoutAsync(LogoutRequestDTO dto, CancellationToken ct);
    }
}