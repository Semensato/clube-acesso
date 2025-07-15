using Application.DTOs.AreaClube;
using Application.DTOs.PlanoAcesso;
using Application.Interfaces;
using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PlanoAcessoService : IPlanoAcessoService
    {
        private readonly IPlanoAcessoRepository _planoAcessoRepository;
        private readonly IAreaClubeRepository _areaClubeRepository;

        public PlanoAcessoService(
            IPlanoAcessoRepository planoAcessoRepository,
            IAreaClubeRepository areaClubeRepository)
        {
            _planoAcessoRepository = planoAcessoRepository;
            _areaClubeRepository = areaClubeRepository;
        }

        public async Task<IEnumerable<PlanoAcessoResponseDto>> ListarAsync()
        {
            var planos = await _planoAcessoRepository.GetAllAsync(includeAreas: true);
            return planos.Select(p => new PlanoAcessoResponseDto
            {
                Id = p.Id,
                Nome = p.Nome,
                Areas = p.Areas.Select(a => new AreaClubeResponseDto
                {
                    Id = a.Id,
                    Nome = a.Nome
                }).ToList()
            });
        }

        public async Task<PlanoAcessoResponseDto?> ObterPorIdAsync(Guid id)
        {
            var plano = await _planoAcessoRepository.GetByIdAsync(id, includeAreas: true);
            if (plano == null) return null;

            return new PlanoAcessoResponseDto
            {
                Id = plano.Id,
                Nome = plano.Nome,
                Areas = plano.Areas.Select(a => new AreaClubeResponseDto
                {
                    Id = a.Id,
                    Nome = a.Nome
                }).ToList()
            };
        }

        public async Task<PlanoAcessoResponseDto?> CriarAsync(PlanoAcessoRequestDto dto)
        {
            // Buscar as áreas existentes no banco
            var areasExistentes = new List<AreaClube>();
            foreach (var areaId in dto.Areas)
            {
                var area = await _areaClubeRepository.GetByIdAsync(areaId);
                if (area == null)
                    throw new KeyNotFoundException($"Área com Id '{areaId}' não encontrada.");
                areasExistentes.Add(area);
            }

            var novoPlano = new PlanoAcesso
            {
                Id = Guid.NewGuid(),
                Nome = dto.Nome,
                Areas = areasExistentes
            };

            await _planoAcessoRepository.AddAsync(novoPlano);
            await _planoAcessoRepository.SaveChangesAsync();

            return new PlanoAcessoResponseDto
            {
                Id = novoPlano.Id,
                Nome = novoPlano.Nome,
                Areas = novoPlano.Areas.Select(a => new AreaClubeResponseDto
                {
                    Id = a.Id,
                    Nome = a.Nome
                }).ToList()
            };
        }

        public async Task AtualizarAsync(Guid id, PlanoAcessoRequestDto dto)
        {
            var planoExistente = await _planoAcessoRepository.GetByIdAsync(id, includeAreas: true);
            if (planoExistente == null)
                throw new KeyNotFoundException("Plano de acesso não encontrado.");

            planoExistente.Nome = dto.Nome;

            // Buscar áreas existentes no banco
            var areasExistentes = new List<AreaClube>();
            foreach (var areaId in dto.Areas)
            {
                var area = await _areaClubeRepository.GetByIdAsync(areaId);
                if (area == null)
                    throw new KeyNotFoundException($"Área com Id '{areaId}' não encontrada.");
                areasExistentes.Add(area);
            }

            // Atualizar a coleção de áreas do plano
            planoExistente.Areas.Clear();
            foreach (var area in areasExistentes)
            {
                planoExistente.Areas.Add(area);
            }

            _planoAcessoRepository.Update(planoExistente);
            await _planoAcessoRepository.SaveChangesAsync();
        }

        public async Task RemoverAsync(Guid id)
        {
            var planoExistente = await _planoAcessoRepository.GetByIdAsync(id);
            if (planoExistente == null)
                throw new KeyNotFoundException("Plano de acesso não encontrado.");

            _planoAcessoRepository.Remove(planoExistente);
            await _planoAcessoRepository.SaveChangesAsync();
        }
    }
}
