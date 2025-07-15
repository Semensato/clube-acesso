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
        public async Task<IActionResult> Listar()
        {
            var areas = await _areaClubeService.ListarAsync();
            return Ok(areas);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var area = await _areaClubeService.ObterPorIdAsync(id);
            if (area == null)
                return NotFound();

            return Ok(area);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] AreaClubeDto dto)
        {
            await _areaClubeService.CriarAsync(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = dto.Id }, dto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] AreaClubeDto dto)
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
