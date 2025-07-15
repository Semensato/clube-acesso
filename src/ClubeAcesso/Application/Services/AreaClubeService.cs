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

        public async Task<IEnumerable<AreaClubeResponseDto>> ListarAsync()
        {
            var areas = await _areaClubeRepository.GetAllAsync();
            return areas.Select(a => new AreaClubeResponseDto
            {
                Id = a.Id,
                Nome = a.Nome
            });
        }

        public async Task<AreaClubeResponseDto?> ObterPorIdAsync(Guid id)
        {
            var area = await _areaClubeRepository.GetByIdAsync(id);
            if (area == null) return null;

            return new AreaClubeResponseDto
            {
                Id = area.Id,
                Nome = area.Nome
            };
        }

        public async Task<AreaClubeResponseDto?> CriarAsync(AreaClubeRequestDto dto)
        {
            var novaArea = new AreaClube
            {
                Id = Guid.NewGuid(),
                Nome = dto.Nome
            };

            await _areaClubeRepository.AddAsync(novaArea);
            await _areaClubeRepository.SaveChangesAsync();

            return new AreaClubeResponseDto
            {
                Id= novaArea.Id,
                Nome = novaArea.Nome
            };
        }

        public async Task AtualizarAsync(Guid id, AreaClubeRequestDto dto)
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
