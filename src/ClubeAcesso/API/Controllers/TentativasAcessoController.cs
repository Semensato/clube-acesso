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
        public async Task<IActionResult> Registrar([FromBody] TentativaAcessoRequestDto dto)
        {
            await _tentativaAcessoService.RegistrarAcessoAsync(dto);
            return Ok(new { mensagem = "Tentativa de acesso registrada com sucesso." });
        }

        /// <summary>
        /// Lista todas as tentativas de acesso de um sócio.
        /// </summary>
        [HttpGet("socio/{socioId:guid}")]
        public async Task<IActionResult> ObterPorSocio(Guid socioId)
        {
            var tentativas = await _tentativaAcessoService.ObterPorSocioIdAsync(socioId);
            return Ok(tentativas);
        }
    }
}
