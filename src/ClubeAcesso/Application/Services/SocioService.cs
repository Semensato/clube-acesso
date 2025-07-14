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
    public class SocioService : ISocioService
    {
        private readonly ISocioRepository _socioRepository;

        public SocioService(ISocioRepository socioRepository)
        {
            this._socioRepository = socioRepository;
        }

        public async Task<IEnumerable<SocioDto>> ListarAsync()
        {
            var socios = await _socioRepository.GetAllAsync();
            return socios.Select(s => new SocioDto
            {
                Id = s.Id,
                Nome = s.Nome,
                Documento = s.Documento
            });
        }

        public async Task<SocioDto?> ObterPorIdAsync(Guid id)
        {
            var socio = await _socioRepository.GetByIdAsync(id);
            if (socio == null) return null;

            return new SocioDto
            {
                Id = socio.Id,
                Nome = socio.Nome,
                Documento = socio.Documento
            };
        }

        public async Task<SocioDto?> ObterPorCpfAsync(string cpf)
        {
            var socio = await _socioRepository.GetByCpfAsync(cpf);
            if (socio == null) return null;

            return new SocioDto
            {
                Id = socio.Id,
                Nome = socio.Nome,
                Documento = socio.Documento
            };
        }

        public async Task CriarAsync(SocioDto dto)
        {
            var novoSocio = new Socio
            {
                Id = Guid.NewGuid(),
                Nome = dto.Nome,
                Documento = dto.Documento
            };

            await _socioRepository.AddAsync(novoSocio);
            await _socioRepository.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Guid id, SocioDto dto)
        {
            var socioExistente = await _socioRepository.GetByIdAsync(id);
            if (socioExistente == null)
                throw new KeyNotFoundException("Sócio não encontrado.");

            socioExistente.Nome = dto.Nome;
            socioExistente.Documento = dto.Documento;

            _socioRepository.Update(socioExistente);
            await _socioRepository.SaveChangesAsync();
        }

        public async Task RemoverAsync(Guid id)
        {
            var socioExistente = await _socioRepository.GetByIdAsync(id);
            if (socioExistente == null)
                throw new KeyNotFoundException("Sócio não encontrado.");

            _socioRepository.Remove(socioExistente);
            await _socioRepository.SaveChangesAsync();
        }
    }
}
