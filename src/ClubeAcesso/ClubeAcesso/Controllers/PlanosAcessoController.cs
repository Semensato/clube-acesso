using Application.DTOs.PlanoAcesso;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanosAcessoController : ControllerBase
    {
        private readonly IPlanoAcessoService _planoAcessoService;

        public PlanosAcessoController(IPlanoAcessoService planoAcessoService)
        {
            _planoAcessoService = planoAcessoService;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var planos = await _planoAcessoService.ListarAsync();
            return Ok(planos);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var plano = await _planoAcessoService.ObterPorIdAsync(id);
            if (plano == null)
                return NotFound();

            return Ok(plano);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] PlanoAcessoRequestDto dto)
        {
            await _planoAcessoService.CriarAsync(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = dto.Id }, dto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] PlanoAcessoRequestDto dto)
        {
            try
            {
                await _planoAcessoService.AtualizarAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Remover(Guid id)
        {
            try
            {
                await _planoAcessoService.RemoverAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
