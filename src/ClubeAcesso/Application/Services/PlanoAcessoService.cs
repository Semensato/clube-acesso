using Application.DTOs;
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
    public class PlanoAcessoService : IPlanoAcessoService
    {
        private readonly IPlanoAcessoRepository _planoAcessoRepository;

        public PlanoAcessoService(IPlanoAcessoRepository planoAcessoRepository)
        {
            _planoAcessoRepository = planoAcessoRepository;
        }

        public async Task<IEnumerable<PlanoAcessoDto>> ListarAsync()
        {
            var planos = await _planoAcessoRepository.GetAllAsync(includeAreas: true);
            return planos.Select(p => new PlanoAcessoDto
            {
                Id = p.Id,
                Nome = p.Nome,
                Areas = p.Areas.Select(a => new AreaClubeDto
                {
                    Id = a.Id,
                    Nome = a.Nome
                }).ToList()
            });
        }

        public async Task<PlanoAcessoDto?> ObterPorIdAsync(Guid id)
        {
            var plano = await _planoAcessoRepository.GetByIdAsync(id, includeAreas: true);
            if (plano == null) return null;

            return new PlanoAcessoDto
            {
                Id = plano.Id,
                Nome = plano.Nome,
                Areas = plano.Areas.Select(a => new AreaClubeDto
                {
                    Id = a.Id,
                    Nome = a.Nome
                }).ToList()
            };
        }

        public async Task CriarAsync(PlanoAcessoDto dto)
        {
            var novoPlano = new PlanoAcesso
            {
                Id = Guid.NewGuid(),
                Nome = dto.Nome,
                Areas = dto.Areas.Select(a => new AreaClube { Id = a.Id, Nome = a.Nome }).ToList()
            };

            await _planoAcessoRepository.AddAsync(novoPlano);
            await _planoAcessoRepository.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Guid id, PlanoAcessoDto dto)
        {
            var planoExistente = await _planoAcessoRepository.GetByIdAsync(id, includeAreas: true);
            if (planoExistente == null)
                throw new KeyNotFoundException("Plano de acesso não encontrado.");

            planoExistente.Nome = dto.Nome;
                        
            planoExistente.Areas.Clear();
            foreach (var areaDto in dto.Areas)
            {
                planoExistente.Areas.Add(new AreaClube
                {
                    Id = areaDto.Id,
                    Nome = areaDto.Nome
                });
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
