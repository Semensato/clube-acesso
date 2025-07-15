using Application.DTOs.AreaClube;
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
        [ProducesResponseType(typeof(PlanoAcessoResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Listar()
        {
            var planos = await _planoAcessoService.ListarAsync();
            return Ok(planos);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(PlanoAcessoResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var plano = await _planoAcessoService.ObterPorIdAsync(id);
            if (plano == null)
                return NotFound();

            return Ok(plano);
        }

        [HttpPost]
        [ProducesResponseType(typeof(PlanoAcessoResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Criar([FromBody] PlanoAcessoRequestDto dto)
        {
            var novoPlano = await _planoAcessoService.CriarAsync(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = novoPlano.Id }, dto);
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
