using Application.DTOs.AreaClube;
using Application.DTOs.PlanoAcesso;
using Application.DTOs.Socio;
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

        public async Task<IEnumerable<SocioResponseDto>> ListarAsync()
        {
            var socios = await _socioRepository.GetAllAsync();
            return socios.Select(s => new SocioResponseDto
            {
                Id = s.Id,
                Nome = s.Nome,
                Documento = s.Documento,
                Plano = new PlanoAcessoResponseDto
                {
                    Id = s.Plano.Id,
                    Nome = s.Plano.Nome,
                    Areas = s.Plano.Areas.Select(a => new AreaClubeResponseDto
                    {
                        Id = a.Id,
                        Nome = a.Nome
                    }).ToList()
                }
            });
        }

        public async Task<SocioResponseDto?> ObterPorIdAsync(Guid id)
        {
            var socio = await _socioRepository.GetByIdAsync(id);
            if (socio == null) return null;

            return new SocioResponseDto
            {
                Id = socio.Id,
                Nome = socio.Nome,
                Documento = socio.Documento,
                Plano = new PlanoAcessoResponseDto
                {
                    Id = socio.Plano.Id,
                    Nome = socio.Plano.Nome,
                    Areas = socio.Plano.Areas.Select(a => new AreaClubeResponseDto
                    {
                        Id = a.Id,
                        Nome = a.Nome
                    }).ToList()
                }
            };
        }

        public async Task<SocioResponseDto?> ObterPorCpfAsync(string cpf)
        {
            var socio = await _socioRepository.GetByCpfAsync(cpf);
            if (socio == null) return null;

            return new SocioResponseDto
            {
                Id = socio.Id,
                Nome = socio.Nome,
                Documento = socio.Documento,
                Plano = new PlanoAcessoResponseDto
                {
                    Id = socio.Plano.Id,
                    Areas = socio.Plano.Areas.Select(a => new AreaClubeResponseDto
                    {
                        Id = a.Id,
                        Nome = a.Nome
                    }).ToList()
                }
            };
        }

        public async Task<SocioResponseDto?> CriarAsync(SocioRequestDto dto)
        {
            var novoSocio = new Socio
            {
                Id = Guid.NewGuid(),
                Nome = dto.Nome,
                Documento = dto.Documento,
                PlanoId = dto.PlanoId
            };

            await _socioRepository.AddAsync(novoSocio);
            await _socioRepository.SaveChangesAsync();

            novoSocio = await _socioRepository.GetByIdAsync(novoSocio.Id);

            if (novoSocio == null)
                return null;

            return new SocioResponseDto
            {
                Id= novoSocio.Id,
                Nome= novoSocio.Nome,
                Documento   = novoSocio.Documento,
                Plano = new PlanoAcessoResponseDto
                {
                    Id = novoSocio.Plano.Id,
                    Areas = novoSocio.Plano.Areas.Select(a => new AreaClubeResponseDto
                    {
                        Id = a.Id,
                        Nome = a.Nome
                    }).ToList()
                }
            };
        }

        public async Task AtualizarAsync(Guid id, SocioRequestDto dto)
        {
            var socioExistente = await _socioRepository.GetByIdAsync(id);
            if (socioExistente == null)
                throw new KeyNotFoundException("Sócio não encontrado.");

            socioExistente.Nome = dto.Nome;
            socioExistente.Documento = dto.Documento;
            socioExistente.PlanoId = dto.PlanoId;

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
