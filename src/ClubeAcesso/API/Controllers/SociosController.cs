using Application.DTOs.Socio;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SociosController : ControllerBase
    {
        private readonly ISocioService _socioService;

        public SociosController(ISocioService socioService)
        {
            _socioService = socioService;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var socios = await _socioService.ListarAsync();
            return Ok(socios);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var socio = await _socioService.ObterPorIdAsync(id);
            if (socio == null)
                return NotFound();

            return Ok(socio);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] SocioRequestDto dto)
        {
            var novoSocio = await _socioService.CriarAsync(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = novoSocio.Id }, dto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] SocioRequestDto dto)
        {
            try
            {
                await _socioService.AtualizarAsync(id, dto);
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
                await _socioService.RemoverAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
