using Application.DTOs.AreaClube;
using Application.Interfaces;
using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AreaClubeService : IAreaClubeService
    {
        private readonly IAreaClubeRepository _areaClubeRepository;

        public AreaClubeService(IAreaClubeRepository areaClubeRepository)
        {
            _areaClubeRepository = areaClubeRepository;
        }

        public async Task<IEnumerable<AreaClubeDto>> ListarAsync()
        {
            var areas = await _areaClubeRepository.GetAllAsync();
            return areas.Select(a => new AreaClubeDto
            {
                Id = a.Id,
                Nome = a.Nome
            });
        }

        public async Task<AreaClubeDto?> ObterPorIdAsync(Guid id)
        {
            var area = await _areaClubeRepository.GetByIdAsync(id);
            if (area == null) return null;

            return new AreaClubeDto
            {
                Id = area.Id,
                Nome = area.Nome
            };
        }

        public async Task CriarAsync(AreaClubeDto dto)
        {
            var novaArea = new AreaClube
            {
                Id = Guid.NewGuid(),
                Nome = dto.Nome
            };

            await _areaClubeRepository.AddAsync(novaArea);
            await _areaClubeRepository.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Guid id, AreaClubeDto dto)
        {
            var areaExistente = await _areaClubeRepository.GetByIdAsync(id);
            if (areaExistente == null)
                throw new KeyNotFoundException("Área do clube não encontrada.");

            areaExistente.Nome = dto.Nome;

            _areaClubeRepository.Update(areaExistente);
            await _areaClubeRepository.SaveChangesAsync();
        }

        public async Task RemoverAsync(Guid id)
        {
            var areaExistente = await _areaClubeRepository.GetByIdAsync(id);
            if (areaExistente == null)
                throw new KeyNotFoundException("Área do clube não encontrada.");

            _areaClubeRepository.Remove(areaExistente);
            await _areaClubeRepository.SaveChangesAsync();
        }
    }
}
