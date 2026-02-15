using Xunit;
using MinhaApi.Dtos;
using MinhaApi.Models;
using System;

namespace MinhaApi.Tests.Dtos
{
    public class LoteMinerioResponseDtoTests
    {
        #region Testes de Criação e Inicialização

        [Fact]
        public void LoteMinerioResponseDto_DeveCriarInstanciaComTodosOsParametros()
        {
            // Arrange
            var dataProducao = new DateTime(2026, 2, 10);

            // Act
            var dto = new LoteMinerioResponseDto(
                Id: 1,
                CodigoLote: "MNA-2026-000123",
                MinaOrigem: "Carajás N4E",
                TeorFe: 67.5m,
                Umidade: 8.2m,
                SiO2: 2.1m,
                P: 0.05m,
                Toneladas: 1500.75m,
                DataProducao: dataProducao,
                Status: StatusLote.EmTransporte,
                LocalizacaoAtual: "EFVM - Trem 123"
            );

            // Assert
            Assert.Equal(1, dto.Id);
            Assert.Equal("MNA-2026-000123", dto.CodigoLote);
            Assert.Equal("Carajás N4E", dto.MinaOrigem);
            Assert.Equal(67.5m, dto.TeorFe);
            Assert.Equal(8.2m, dto.Umidade);
            Assert.Equal(2.1m, dto.SiO2);
            Assert.Equal(0.05m, dto.P);
            Assert.Equal(1500.75m, dto.Toneladas);
            Assert.Equal(dataProducao, dto.DataProducao);
            Assert.Equal(StatusLote.EmTransporte, dto.Status);
            Assert.Equal("EFVM - Trem 123", dto.LocalizacaoAtual);
        }

        [Fact]
        public void LoteMinerioResponseDto_DeveCriarComPropriedadesNulaveisNulas()
        {
            // Arrange & Act
            var dto = new LoteMinerioResponseDto(
                Id: 1,
                CodigoLote: "MNA-2026-000001",
                MinaOrigem: "Carajás N4E",
                TeorFe: 67.5m,
                Umidade: 8.0m,
                SiO2: null,
                P: null,
                Toneladas: 2000m,
                DataProducao: DateTime.Now,
                Status: StatusLote.EmEstoque,
                LocalizacaoAtual: "Pátio Carajás"
            );

            // Assert
            Assert.Null(dto.SiO2);
            Assert.Null(dto.P);
        }

        #endregion

        #region Testes de Imutabilidade (Record)

        [Fact]
        public void LoteMinerioResponseDto_DeveSerRecord()
        {
            // Arrange
            var type = typeof(LoteMinerioResponseDto);

            // Assert
            Assert.True(type.IsValueType == false); // Records são classes
            Assert.Contains(type.GetMethods(), m => m.Name == "<Clone>$");
        }

        [Fact]
        public void LoteMinerioResponseDto_DevePermitirCriacaoComWith()
        {
            // Arrange
            var dtoOriginal = new LoteMinerioResponseDto(
                Id: 1,
                CodigoLote: "MNA-2026-000001",
                MinaOrigem: "Carajás N4E",
                TeorFe: 67.5m,
                Umidade: 8.0m,
                SiO2: 2.0m,
                P: 0.04m,
                Toneladas: 2000m,
                DataProducao: new DateTime(2026, 2, 1),
                Status: StatusLote.EmEstoque,
                LocalizacaoAtual: "Pátio Carajás"
            );

            // Act
            var dtoModificado = dtoOriginal with 
            { 
                Status = StatusLote.EmTransporte,
                LocalizacaoAtual = "EFVM - Trem 789"
            };

            // Assert
            Assert.Equal(StatusLote.EmEstoque, dtoOriginal.Status);
            Assert.Equal("Pátio Carajás", dtoOriginal.LocalizacaoAtual);
            Assert.Equal(StatusLote.EmTransporte, dtoModificado.Status);
            Assert.Equal("EFVM - Trem 789", dtoModificado.LocalizacaoAtual);
            Assert.Equal(dtoOriginal.Id, dtoModificado.Id);
            Assert.Equal(dtoOriginal.CodigoLote, dtoModificado.CodigoLote);
        }

        #endregion

        #region Testes de Igualdade (Record Equality)

        [Fact]
        public void LoteMinerioResponseDto_DoisDtosComMesmosValores_DevemSerIguais()
        {
            // Arrange
            var dataProducao = new DateTime(2026, 2, 10);
            var dto1 = new LoteMinerioResponseDto(
                Id: 1,
                CodigoLote: "MNA-2026-000123",
                MinaOrigem: "Carajás N4E",
                TeorFe: 67.5m,
                Umidade: 8.2m,
                SiO2: 2.1m,
                P: 0.05m,
                Toneladas: 1500.75m,
                DataProducao: dataProducao,
                Status: StatusLote.EmTransporte,
                LocalizacaoAtual: "EFVM - Trem 123"
            );

            var dto2 = new LoteMinerioResponseDto(
                Id: 1,
                CodigoLote: "MNA-2026-000123",
                MinaOrigem: "Carajás N4E",
                TeorFe: 67.5m,
                Umidade: 8.2m,
                SiO2: 2.1m,
                P: 0.05m,
                Toneladas: 1500.75m,
                DataProducao: dataProducao,
                Status: StatusLote.EmTransporte,
                LocalizacaoAtual: "EFVM - Trem 123"
            );

            // Assert
            Assert.Equal(dto1, dto2);
            Assert.True(dto1 == dto2);
            Assert.False(dto1 != dto2);
        }

        [Fact]
        public void LoteMinerioResponseDto_DoisDtosComValoresDiferentes_NaoDevemSerIguais()
        {
            // Arrange
            var dto1 = new LoteMinerioResponseDto(
                Id: 1,
                CodigoLote: "MNA-2026-000123",
                MinaOrigem: "Carajás N4E",
                TeorFe: 67.5m,
                Umidade: 8.2m,
                SiO2: 2.1m,
                P: 0.05m,
                Toneladas: 1500.75m,
                DataProducao: DateTime.Now,
                Status: StatusLote.EmTransporte,
                LocalizacaoAtual: "EFVM - Trem 123"
            );

            var dto2 = new LoteMinerioResponseDto(
                Id: 2,
                CodigoLote: "MNA-2026-000456",
                MinaOrigem: "Carajás S11D",
                TeorFe: 66.0m,
                Umidade: 9.0m,
                SiO2: 2.5m,
                P: 0.06m,
                Toneladas: 1800m,
                DataProducao: DateTime.Now,
                Status: StatusLote.EmEstoque,
                LocalizacaoAtual: "Pátio Carajás"
            );

            // Assert
            Assert.NotEqual(dto1, dto2);
            Assert.False(dto1 == dto2);
            Assert.True(dto1 != dto2);
        }

        [Fact]
        public void LoteMinerioResponseDto_DtosComPropriedadesNulaveisNulas_DevemSerIguais()
        {
            // Arrange
            var data = new DateTime(2026, 2, 10);
            var dto1 = new LoteMinerioResponseDto(
                Id: 1,
                CodigoLote: "MNA-2026-000001",
                MinaOrigem: "Carajás N4E",
                TeorFe: 67.5m,
                Umidade: 8.0m,
                SiO2: null,
                P: null,
                Toneladas: 2000m,
                DataProducao: data,
                Status: StatusLote.EmEstoque,
                LocalizacaoAtual: "Pátio"
            );

            var dto2 = new LoteMinerioResponseDto(
                Id: 1,
                CodigoLote: "MNA-2026-000001",
                MinaOrigem: "Carajás N4E",
                TeorFe: 67.5m,
                Umidade: 8.0m,
                SiO2: null,
                P: null,
                Toneladas: 2000m,
                DataProducao: data,
                Status: StatusLote.EmEstoque,
                LocalizacaoAtual: "Pátio"
            );

            // Assert
            Assert.Equal(dto1, dto2);
        }

        #endregion

        #region Testes de ToString e GetHashCode

        [Fact]
        public void LoteMinerioResponseDto_ToString_DeveConterTodasAsPropriedades()
        {
            // Arrange
            var dto = new LoteMinerioResponseDto(
                Id: 1,
                CodigoLote: "MNA-2026-000123",
                MinaOrigem: "Carajás N4E",
                TeorFe: 67.5m,
                Umidade: 8.2m,
                SiO2: 2.1m,
                P: 0.05m,
                Toneladas: 1500.75m,
                DataProducao: new DateTime(2026, 2, 10),
                Status: StatusLote.EmTransporte,
                LocalizacaoAtual: "EFVM - Trem 123"
            );

            // Act
            var toString = dto.ToString();

            // Assert
            Assert.Contains("1", toString);
            Assert.Contains("MNA-2026-000123", toString);
            Assert.Contains("Carajás N4E", toString);
        }

        [Fact]
        public void LoteMinerioResponseDto_GetHashCode_DtosIguaisDevemTerMesmoHashCode()
        {
            // Arrange
            var data = new DateTime(2026, 2, 10);
            var dto1 = new LoteMinerioResponseDto(
                Id: 1,
                CodigoLote: "MNA-2026-000123",
                MinaOrigem: "Carajás N4E",
                TeorFe: 67.5m,
                Umidade: 8.2m,
                SiO2: 2.1m,
                P: 0.05m,
                Toneladas: 1500.75m,
                DataProducao: data,
                Status: StatusLote.EmTransporte,
                LocalizacaoAtual: "EFVM - Trem 123"
            );

            var dto2 = new LoteMinerioResponseDto(
                Id: 1,
                CodigoLote: "MNA-2026-000123",
                MinaOrigem: "Carajás N4E",
                TeorFe: 67.5m,
                Umidade: 8.2m,
                SiO2: 2.1m,
                P: 0.05m,
                Toneladas: 1500.75m,
                DataProducao: data,
                Status: StatusLote.EmTransporte,
                LocalizacaoAtual: "EFVM - Trem 123"
            );

            // Assert
            Assert.Equal(dto1.GetHashCode(), dto2.GetHashCode());
        }

        #endregion

        #region Testes de Valores de Propriedades

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(999999)]
        public void LoteMinerioResponseDto_Id_DeveAceitarDiferentesValores(int id)
        {
            // Arrange & Act
            var dto = new LoteMinerioResponseDto(
                Id: id,
                CodigoLote: "MNA-2026-000001",
                MinaOrigem: "Carajás N4E",
                TeorFe: 67.5m,
                Umidade: 8.0m,
                SiO2: null,
                P: null,
                Toneladas: 2000m,
                DataProducao: DateTime.Now,
                Status: StatusLote.EmEstoque,
                LocalizacaoAtual: "Pátio"
            );

            // Assert
            Assert.Equal(id, dto.Id);
        }

        [Theory]
        [InlineData("MNA-2026-000001")]
        [InlineData("MNA-2026-999999")]
        [InlineData("ABC-2025-123456")]
        public void LoteMinerioResponseDto_CodigoLote_DeveAceitarDiferentesFormatos(string codigo)
        {
            // Arrange & Act
            var dto = new LoteMinerioResponseDto(
                Id: 1,
                CodigoLote: codigo,
                MinaOrigem: "Carajás N4E",
                TeorFe: 67.5m,
                Umidade: 8.0m,
                SiO2: null,
                P: null,
                Toneladas: 2000m,
                DataProducao: DateTime.Now,
                Status: StatusLote.EmEstoque,
                LocalizacaoAtual: "Pátio"
            );

            // Assert
            Assert.Equal(codigo, dto.CodigoLote);
        }

        [Theory]
        [InlineData("Carajás N4E")]
        [InlineData("Carajás S11D")]
        [InlineData("Brucutu")]
        [InlineData("Vargem Grande")]
        public void LoteMinerioResponseDto_MinaOrigem_DeveAceitarDiferentesMinas(string mina)
        {
            // Arrange & Act
            var dto = new LoteMinerioResponseDto(
                Id: 1,
                CodigoLote: "MNA-2026-000001",
                MinaOrigem: mina,
                TeorFe: 67.5m,
                Umidade: 8.0m,
                SiO2: null,
                P: null,
                Toneladas: 2000m,
                DataProducao: DateTime.Now,
                Status: StatusLote.EmEstoque,
                LocalizacaoAtual: "Pátio"
            );

            // Assert
            Assert.Equal(mina, dto.MinaOrigem);
        }

        [Theory]
        [InlineData(StatusLote.EmEstoque)]
        [InlineData(StatusLote.EmTransporte)]
        [InlineData(StatusLote.Embarcado)]
        public void LoteMinerioResponseDto_Status_DeveAceitarTodosOsStatusPossiveis(StatusLote status)
        {
            // Arrange & Act
            var dto = new LoteMinerioResponseDto(
                Id: 1,
                CodigoLote: "MNA-2026-000001",
                MinaOrigem: "Carajás N4E",
                TeorFe: 67.5m,
                Umidade: 8.0m,
                SiO2: null,
                P: null,
                Toneladas: 2000m,
                DataProducao: DateTime.Now,
                Status: status,
                LocalizacaoAtual: "Pátio"
            );

            // Assert
            Assert.Equal(status, dto.Status);
        }

        #endregion

        #region Testes de Cenários Completos

        [Fact]
        public void LoteMinerioResponseDto_CenarioCompleto_LoteAltaQualidadeEmEstoque()
        {
            // Arrange & Act
            var dto = new LoteMinerioResponseDto(
                Id: 1,
                CodigoLote: "MNA-2026-000001",
                MinaOrigem: "Carajás N4E",
                TeorFe: 68.5m,
                Umidade: 7.5m,
                SiO2: 1.5m,
                P: 0.03m,
                Toneladas: 2000m,
                DataProducao: new DateTime(2026, 2, 1),
                Status: StatusLote.EmEstoque,
                LocalizacaoAtual: "Pátio Carajás"
            );

            // Assert
            Assert.True(dto.TeorFe > 67); // Alta qualidade
            Assert.True(dto.Umidade < 8); // Baixa umidade
            Assert.Equal(StatusLote.EmEstoque, dto.Status);
            Assert.NotNull(dto.SiO2);
            Assert.NotNull(dto.P);
        }

        [Fact]
        public void LoteMinerioResponseDto_CenarioCompleto_LoteEmTransporteFerrovia()
        {
            // Arrange & Act
            var dto = new LoteMinerioResponseDto(
                Id: 2,
                CodigoLote: "MNA-2026-000002",
                MinaOrigem: "Carajás S11D",
                TeorFe: 66.8m,
                Umidade: 8.5m,
                SiO2: 2.0m,
                P: null,
                Toneladas: 1800m,
                DataProducao: new DateTime(2026, 2, 5),
                Status: StatusLote.EmTransporte,
                LocalizacaoAtual: "EFVM - Trem 456"
            );

            // Assert
            Assert.Equal(StatusLote.EmTransporte, dto.Status);
            Assert.Contains("EFVM", dto.LocalizacaoAtual);
            Assert.Null(dto.P);
        }

        [Fact]
        public void LoteMinerioResponseDto_CenarioCompleto_LoteEmbarcadoNoPorto()
        {
            // Arrange & Act
            var dto = new LoteMinerioResponseDto(
                Id: 3,
                CodigoLote: "MNA-2026-000003",
                MinaOrigem: "Brucutu",
                TeorFe: 65.2m,
                Umidade: 9.2m,
                SiO2: null,
                P: null,
                Toneladas: 2500m,
                DataProducao: new DateTime(2026, 1, 28),
                Status: StatusLote.Embarcado,
                LocalizacaoAtual: "Porto Tubarão - Navio MV Iron Star"
            );

            // Assert
            Assert.Equal(StatusLote.Embarcado, dto.Status);
            Assert.Contains("Porto", dto.LocalizacaoAtual);
            Assert.Null(dto.SiO2);
            Assert.Null(dto.P);
        }

        #endregion

        #region Testes de Conversão/Mapeamento (Sugestão)

        [Fact]
        public void LoteMinerioResponseDto_DevePodeSeCriadoAPartirDeEntidade()
        {
            // Arrange
            var entidade = new LoteMinerio
            {
                Id = 1,
                CodigoLote = "MNA-2026-000123",
                MinaOrigem = "Carajás N4E",
                TeorFe = 67.5m,
                Umidade = 8.2m,
                SiO2 = 2.1m,
                P = 0.05m,
                Toneladas = 1500.75m,
                DataProducao = new DateTime(2026, 2, 10),
                Status = StatusLote.EmTransporte,
                LocalizacaoAtual = "EFVM - Trem 123"
            };

            // Act
            var dto = new LoteMinerioResponseDto(
                Id: entidade.Id,
                CodigoLote: entidade.CodigoLote,
                MinaOrigem: entidade.MinaOrigem,
                TeorFe: entidade.TeorFe,
                Umidade: entidade.Umidade,
                SiO2: entidade.SiO2,
                P: entidade.P,
                Toneladas: entidade.Toneladas,
                DataProducao: entidade.DataProducao,
                Status: entidade.Status,
                LocalizacaoAtual: entidade.LocalizacaoAtual
            );

            // Assert
            Assert.Equal(entidade.Id, dto.Id);
            Assert.Equal(entidade.CodigoLote, dto.CodigoLote);
            Assert.Equal(entidade.MinaOrigem, dto.MinaOrigem);
            Assert.Equal(entidade.TeorFe, dto.TeorFe);
            Assert.Equal(entidade.Umidade, dto.Umidade);
            Assert.Equal(entidade.SiO2, dto.SiO2);
            Assert.Equal(entidade.P, dto.P);
            Assert.Equal(entidade.Toneladas, dto.Toneladas);
            Assert.Equal(entidade.DataProducao, dto.DataProducao);
            Assert.Equal(entidade.Status, dto.Status);
            Assert.Equal(entidade.LocalizacaoAtual, dto.LocalizacaoAtual);
        }

        #endregion

        #region Testes de Desconstrução

        [Fact]
        public void LoteMinerioResponseDto_DevePermitirDesconstrucao()
        {
            // Arrange
            var dto = new LoteMinerioResponseDto(
                Id: 1,
                CodigoLote: "MNA-2026-000123",
                MinaOrigem: "Carajás N4E",
                TeorFe: 67.5m,
                Umidade: 8.2m,
                SiO2: 2.1m,
                P: 0.05m,
                Toneladas: 1500.75m,
                DataProducao: new DateTime(2026, 2, 10),
                Status: StatusLote.EmTransporte,
                LocalizacaoAtual: "EFVM - Trem 123"
            );

            // Act
            var (id, codigo, mina, teor, umidade, sio2, p, toneladas, data, status, localizacao) = dto;

            // Assert
            Assert.Equal(1, id);
            Assert.Equal("MNA-2026-000123", codigo);
            Assert.Equal("Carajás N4E", mina);
            Assert.Equal(67.5m, teor);
            Assert.Equal(8.2m, umidade);
            Assert.Equal(2.1m, sio2);
            Assert.Equal(0.05m, p);
            Assert.Equal(1500.75m, toneladas);
            Assert.Equal(new DateTime(2026, 2, 10), data);
            Assert.Equal(StatusLote.EmTransporte, status);
            Assert.Equal("EFVM - Trem 123", localizacao);
        }

        #endregion
    }
}