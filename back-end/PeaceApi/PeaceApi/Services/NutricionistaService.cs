// PeaceApi/Services/INutricionistaService.cs
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using PeaceApi.DTOs;

namespace PeaceApi.Services
{
    /// <summary>
    /// Operações de leitura/escrita sobre o agregado Nutricionista.
    /// Inclui políticas de permissão: o próprio Nutricionista administra sua conta.
    /// </summary>
    public interface INutricionistaService
    {
        /// <summary>
        /// Retorna o perfil do usuário autenticado (a partir dos claims).
        /// </summary>
        Task<NutricionistaResponseDTO?> GetCurrentAsync(ClaimsPrincipal user, CancellationToken ct);

        /// <summary>
        /// Lista (paginada) com busca opcional e possibilidade de incluir soft-deletados.
        /// </summary>
        Task<PagedResultDTO<NutricionistaListItemDTO>> ListAsync(
            int page,
            int pageSize,
            string? search,
            bool includeDeleted,
            CancellationToken ct);

        /// <summary>
        /// Busca por Id.
        /// </summary>
        Task<NutricionistaResponseDTO?> GetByIdAsync(Guid id, CancellationToken ct);

        /// <summary>
        /// Cria um nutricionista (fluxo administrativo do próprio Nutricionista, se o domínio permitir multi-conta).
        /// </summary>
        Task<NutricionistaResponseDTO> CreateAsync(CreateNutricionistaDTO dto, CancellationToken ct);

        /// <summary>
        /// Atualiza dados do perfil. Deve validar se currentUser é o “dono” do recurso.
        /// </summary>
        Task<NutricionistaResponseDTO?> UpdateAsync(
            Guid id,
            UpdateNutricionistaDTO dto,
            ClaimsPrincipal currentUser,
            CancellationToken ct);

        /// <summary>
        /// Altera senha com validação de senha atual (quando aplicável).
        /// Deve respeitar permissões (o usuário só altera a própria).
        /// </summary>
        Task<bool> ChangePasswordAsync(
            Guid id,
            ChangePasswordDTO dto,
            ClaimsPrincipal currentUser,
            CancellationToken ct);

        /// <summary>
        /// Soft delete do nutricionista.
        /// </summary>
        Task<bool> DeleteAsync(Guid id, CancellationToken ct);

        /// <summary>
        /// Restaura um registro soft-deletado.
        /// </summary>
        Task<bool> RestoreAsync(Guid id, CancellationToken ct);

        /// <summary>
        /// Alterna o status Ativo (true/false). Retorna o novo status ou null se não encontrado.
        /// </summary>
        Task<bool?> ToggleAtivoAsync(Guid id, CancellationToken ct);
    }
}