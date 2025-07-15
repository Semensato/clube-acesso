using Application.DTOs.AreaClube;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AreasClubeController : ControllerBase
    {
        private readonly IAreaClubeService _areaClubeService;

        public AreasClubeController(IAreaClubeService areaClubeService)
        {
            _areaClubeService = areaClubeService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(AreaClubeResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Listar()
        {
            var areas = await _areaClubeService.ListarAsync();
            return Ok(areas);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(AreaClubeResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var area = await _areaClubeService.ObterPorIdAsync(id);
            if (area == null)
                return NotFound();

            return Ok(area);
        }

        [HttpPost]
        [ProducesResponseType(typeof(AreaClubeResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Criar([FromBody] AreaClubeRequestDto dto)
        {
            var novaArea = await _areaClubeService.CriarAsync(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = novaArea.Id }, dto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] AreaClubeRequestDto dto)
        {
            try
            {
                await _areaClubeService.AtualizarAsync(id, dto);
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
                await _areaClubeService.RemoverAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
