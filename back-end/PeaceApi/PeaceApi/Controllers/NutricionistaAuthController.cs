using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeaceApi.DTOs;
using PeaceApi.Services;

namespace PeaceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NutricionistaAuthController : ControllerBase
    {
        private readonly INutricionistaService _nutriService;
        private readonly INutricionistaAuthService _authService;

        public NutricionistaAuthController(
            INutricionistaService nutriService,
            INutricionistaAuthService authService)
        {
            _nutriService = nutriService;
            _authService = authService;
        }

        // Auth
        // =============================

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterNutricionistaDTO dto, CancellationToken ct)
        {
            var result = await _authService.RegisterAsync(dto, ct);
            if (result == null) return BadRequest("Email já cadastrado.");

            return Ok(new
            {
                result.NomeCompleto,
                result.Email,
                result.CRN
            });
        }

       

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDTO dto, CancellationToken ct)
        {
            var result = await _authService.RefreshTokenAsync(dto, ct);
            if (result == null) return Unauthorized("Refresh token inválido ou expirado.");
            return Ok(result);
        }

        [HttpPost("logout")]
        [Authorize(Roles = "Nutricionista")]
        public async Task<IActionResult> Logout([FromBody] LogoutRequestDTO dto, CancellationToken ct)
        {
            await _authService.LogoutAsync(dto, ct);
            return NoContent();
        }

        [HttpGet("me")]
        [Authorize(Roles = "Nutricionista")]
        public async Task<IActionResult> Me(CancellationToken ct)
        {
            var me = await _nutriService.GetCurrentAsync(User, ct);
            if (me == null) return NotFound();
            return Ok(me);
        }

        // =============================
        // CRUD de Nutricionista
        // (o próprio Nutricionista é quem “administra”)
        // =============================

        [HttpGet]
        [Authorize(Roles = "Nutricionista")]
        public async Task<IActionResult> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] string? search = null,
            [FromQuery] bool includeDeleted = false,
            CancellationToken ct = default)
        {
            if (page < 1) page = 1;
            if (pageSize < 1 || pageSize > 100) pageSize = 20;

            var result = await _nutriService.ListAsync(page, pageSize, search, includeDeleted, ct);
            return Ok(result); // PagedResultDTO<NutricionistaListItemDTO>
        }

        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Nutricionista")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var item = await _nutriService.GetByIdAsync(id, ct);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // Criação de outro Nutricionista (se seu domínio permitir multi-conta)
        [HttpPost]
        [Authorize(Roles = "Nutricionista")]
        public async Task<IActionResult> Create([FromBody] CreateNutricionistaDTO dto, CancellationToken ct)
        {
            var created = await _nutriService.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // Update de perfil (aplique validação no service para só permitir o “dono” editar-se)
        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Nutricionista")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateNutricionistaDTO dto, CancellationToken ct)
        {
            var updated = await _nutriService.UpdateAsync(id, dto, User, ct);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        // Alteração de senha
        [HttpPatch("{id:guid}/alterar-senha")]
        [Authorize(Roles = "Nutricionista")]
        public async Task<IActionResult> ChangePassword(Guid id, [FromBody] ChangePasswordDTO dto, CancellationToken ct)
        {
            var ok = await _nutriService.ChangePasswordAsync(id, dto, User, ct);
            if (!ok) return BadRequest("Não foi possível alterar a senha (verifique senha atual/permissões).");
            return NoContent();
        }

        // Soft delete
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Nutricionista")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var ok = await _nutriService.DeleteAsync(id, ct);
            if (!ok) return NotFound();
            return NoContent();
        }

        // Restaurar (soft delete)
        [HttpPost("{id:guid}/restore")]
        [Authorize(Roles = "Nutricionista")]
        public async Task<IActionResult> Restore(Guid id, CancellationToken ct)
        {
            var ok = await _nutriService.RestoreAsync(id, ct);
            if (!ok) return NotFound();
            return NoContent();
        }

        // Ativar/Desativar
        [HttpPost("{id:guid}/toggle-ativo")]
        [Authorize(Roles = "Nutricionista")]
        public async Task<IActionResult> ToggleAtivo(Guid id, CancellationToken ct)
        {
            var status = await _nutriService.ToggleAtivoAsync(id, ct);
            if (status == null) return NotFound();
            return Ok(status); // novo status (bool Ativo)
        }
    }
}