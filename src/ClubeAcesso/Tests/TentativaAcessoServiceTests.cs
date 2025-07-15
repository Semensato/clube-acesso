using Application.DTOs.TentativaAcesso;
using Application.Services;
using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class TentativaAcessoServiceTests
    {
        private readonly Mock<ITentativaAcessoRepository> _tentativaRepoMock = new();
        private readonly Mock<ISocioRepository> _socioRepoMock = new();
        private readonly Mock<IPlanoAcessoRepository> _planoRepoMock = new();
        private readonly Mock<IAreaClubeRepository> _areaRepoMock = new();

        private readonly TentativaAcessoService _service;

        public TentativaAcessoServiceTests()
        {
            _service = new TentativaAcessoService(
                _tentativaRepoMock.Object,
                _socioRepoMock.Object,
                _planoRepoMock.Object,
                _areaRepoMock.Object
            );
        }

        [Fact]
        public async Task RegistrarAcessoAsync_DeveAutorizarSeAreaEstiverNoPlano()
        {
            // Arrange
            var socioId = Guid.NewGuid();
            var areaId = Guid.NewGuid();
            var planoId = Guid.NewGuid();

            var dto = new TentativaAcessoRequestDto { SocioId = socioId, AreaId = areaId };

            _socioRepoMock.Setup(r => r.GetByIdAsync(socioId))
                .ReturnsAsync(new Socio { Id = socioId, Nome = "Gui", PlanoId = planoId });

            _areaRepoMock.Setup(r => r.GetByIdAsync(areaId))
                .ReturnsAsync(new AreaClube { Id = areaId, Nome = "Piscina" });

            _planoRepoMock.Setup(r => r.GetByIdAsync(planoId, true))
                .ReturnsAsync(new PlanoAcesso
                {
                    Id = planoId,
                    Nome = "Premium",
                    Areas = new List<AreaClube> { new AreaClube { Id = areaId } }
                });

            TentativaAcesso tentativaRegistrada = null;
            _tentativaRepoMock.Setup(r => r.AddAsync(It.IsAny<TentativaAcesso>()))
                .Callback<TentativaAcesso>(t => tentativaRegistrada = t);

            // Act
            await _service.RegistrarAcessoAsync(dto);

            // Assert
            Assert.NotNull(tentativaRegistrada);
            Assert.Equal(ResultadoTentativa.Autorizado, tentativaRegistrada.Resultado);
            Assert.Equal(socioId, tentativaRegistrada.SocioId);
            Assert.Equal(areaId, tentativaRegistrada.AreaId);
        }

        [Fact]
        public async Task RegistrarAcessoAsync_DeveNegarSeAreaNaoEstiverNoPlano()
        {
            // Arrange
            var socioId = Guid.NewGuid();
            var areaId = Guid.NewGuid();
            var planoId = Guid.NewGuid();

            var dto = new TentativaAcessoRequestDto { SocioId = socioId, AreaId = areaId };

            _socioRepoMock.Setup(r => r.GetByIdAsync(socioId))
                .ReturnsAsync(new Socio { Id = socioId, Nome = "Gui", PlanoId = planoId });

            _areaRepoMock.Setup(r => r.GetByIdAsync(areaId))
                .ReturnsAsync(new AreaClube { Id = areaId, Nome = "Academia" });

            _planoRepoMock.Setup(r => r.GetByIdAsync(planoId, true))
                .ReturnsAsync(new PlanoAcesso
                {
                    Id = planoId,
                    Nome = "Plano Piscina",
                    Areas = new List<AreaClube> { new AreaClube { Id = Guid.NewGuid() } } // área diferente
                });

            TentativaAcesso tentativaRegistrada = null;
            _tentativaRepoMock.Setup(r => r.AddAsync(It.IsAny<TentativaAcesso>()))
                .Callback<TentativaAcesso>(t => tentativaRegistrada = t);

            // Act
            await _service.RegistrarAcessoAsync(dto);

            // Assert
            Assert.NotNull(tentativaRegistrada);
            Assert.Equal(ResultadoTentativa.Negado, tentativaRegistrada.Resultado);
        }
    }
}
