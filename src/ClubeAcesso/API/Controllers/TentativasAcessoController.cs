using Application.DTOs.Socio;
using Application.DTOs.TentativaAcesso;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TentativasAcessoController : ControllerBase
    {
        private readonly ITentativaAcessoService _tentativaAcessoService;

        public TentativasAcessoController(ITentativaAcessoService tentativaAcessoService)
        {
            _tentativaAcessoService = tentativaAcessoService;
        }

        /// <summary>
        /// Registra uma tentativa de acesso por um sócio a uma área do clube.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(TentativaAcessoResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Registrar([FromBody] TentativaAcessoRequestDto dto)
        {
            var tentativaAcessso = await _tentativaAcessoService.RegistrarAcessoAsync(dto);
            return Ok(tentativaAcessso);
        }

        /// <summary>
        /// Lista todas as tentativas de acesso de um sócio.
        /// </summary>
        [HttpGet("socio/{socioId:guid}")]
        [ProducesResponseType(typeof(TentativaAcessoResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterPorSocio(Guid socioId)
        {
            var tentativas = await _tentativaAcessoService.ObterPorSocioIdAsync(socioId);
            return Ok(tentativas);
        }
    }
}
