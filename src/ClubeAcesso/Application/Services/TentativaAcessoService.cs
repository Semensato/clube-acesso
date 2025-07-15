using Application.DTOs.TentativaAcesso;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TentativaAcessoService : ITentativaAcessoService
    {
        private readonly ITentativaAcessoRepository _tentativaAcessoRepository;
        private readonly ISocioRepository _socioRepository;
        private readonly IPlanoAcessoRepository _planoAcessoRepository;
        private readonly IAreaClubeRepository _areaClubeRepository;

        public TentativaAcessoService(
            ITentativaAcessoRepository tentativaAcessoRepository,
            ISocioRepository socioRepository,
            IPlanoAcessoRepository planoAcessoRepository,
            IAreaClubeRepository areaClubeRepository)
        {
            _tentativaAcessoRepository = tentativaAcessoRepository;
            _socioRepository = socioRepository;
            _planoAcessoRepository = planoAcessoRepository;
            _areaClubeRepository = areaClubeRepository;
        }

        public async Task<TentativaAcessoResponseDto?> RegistrarAcessoAsync(TentativaAcessoRequestDto dto)
        {
            // Busca sócio
            var socio = await _socioRepository.GetByIdAsync(dto.SocioId);
            if (socio == null)
                throw new KeyNotFoundException("Sócio não encontrado.");

            // Busca área do clube
            var area = await _areaClubeRepository.GetByIdAsync(dto.AreaId);
            if (area == null)
                throw new KeyNotFoundException("Área do clube não encontrada.");

            // Busca plano do sócio, incluindo áreas
            var plano = await _planoAcessoRepository.GetByIdAsync(socio.PlanoId, includeAreas: true);
            if (plano == null)
                throw new KeyNotFoundException("Plano de acesso do sócio não encontrado.");

            // Verifica se o plano permite acesso à área
            bool permitido = plano.Areas.Any(a => a.Id == dto.AreaId);

            var tentativa = new TentativaAcesso
            {
                Id = Guid.NewGuid(),
                SocioId = dto.SocioId,
                AreaId = dto.AreaId,
                DataHora = DateTime.UtcNow,
                Resultado = permitido ? ResultadoTentativa.Autorizado : ResultadoTentativa.Negado
            };

            await _tentativaAcessoRepository.AddAsync(tentativa);
            await _tentativaAcessoRepository.SaveChangesAsync();

            return new TentativaAcessoResponseDto
            {
                Id = tentativa.Id,
                SocioId = tentativa.SocioId,
                AreaId = tentativa.AreaId,
                DataHora = tentativa.DataHora,
                Resultado = tentativa.Resultado
            };
        }

        public async Task<IEnumerable<TentativaAcessoResponseDto>> ObterPorSocioIdAsync(Guid socioId)
        {
            var tentativas = await _tentativaAcessoRepository.GetBySocioIdAsync(socioId);
            return tentativas.Select(t => new TentativaAcessoResponseDto
            {
                Id = t.Id,
                SocioId = t.SocioId,
                AreaId = t.AreaId,
                DataHora = t.DataHora,
                Resultado = t.Resultado
            });
        }
    }
}
