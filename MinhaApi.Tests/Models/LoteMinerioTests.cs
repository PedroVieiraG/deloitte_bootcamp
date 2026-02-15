using Xunit;
using MinhaApi.Models;
using System;

namespace MinhaApi.Tests.Models
{
    public class LoteMinerioTests
    {
        #region Testes de Criação e Inicialização

        [Fact]
        public void LoteMinerio_DeveCriarInstanciaComValoresPadrao()
        {
            // Arrange & Act
            var lote = new LoteMinerio();

            // Assert
            Assert.Equal(0, lote.Id);
            Assert.Equal(string.Empty, lote.CodigoLote);
            Assert.Equal(string.Empty, lote.MinaOrigem);
            Assert.Equal(0, lote.TeorFe);
            Assert.Equal(0, lote.Umidade);
            Assert.Null(lote.SiO2);
            Assert.Null(lote.P);
            Assert.Equal(0, lote.Toneladas);
            Assert.Equal(default(DateTime), lote.DataProducao);
            Assert.Equal(StatusLote.EmEstoque, lote.Status);
            Assert.Equal(string.Empty, lote.LocalizacaoAtual);
        }

        [Fact]
        public void LoteMinerio_DevePermitirAtribuicaoDePropriedades()
        {
            // Arrange
            var dataProducao = new DateTime(2026, 2, 10);
            var lote = new LoteMinerio();

            // Act
            lote.Id = 1;
            lote.CodigoLote = "MNA-2026-000123";
            lote.MinaOrigem = "Carajás N4E";
            lote.TeorFe = 67.5m;
            lote.Umidade = 8.2m;
            lote.SiO2 = 2.1m;
            lote.P = 0.05m;
            lote.Toneladas = 1500.75m;
            lote.DataProducao = dataProducao;
            lote.Status = StatusLote.EmTransporte;
            lote.LocalizacaoAtual = "EFVM - Trem 123";

            // Assert
            Assert.Equal(1, lote.Id);
            Assert.Equal("MNA-2026-000123", lote.CodigoLote);
            Assert.Equal("Carajás N4E", lote.MinaOrigem);
            Assert.Equal(67.5m, lote.TeorFe);
            Assert.Equal(8.2m, lote.Umidade);
            Assert.Equal(2.1m, lote.SiO2);
            Assert.Equal(0.05m, lote.P);
            Assert.Equal(1500.75m, lote.Toneladas);
            Assert.Equal(dataProducao, lote.DataProducao);
            Assert.Equal(StatusLote.EmTransporte, lote.Status);
            Assert.Equal("EFVM - Trem 123", lote.LocalizacaoAtual);
        }

        #endregion

        #region Testes de Qualidade do Minério

        [Theory]
        [InlineData(0)]
        [InlineData(50.5)]
        [InlineData(67.8)]
        [InlineData(100)]
        public void TeorFe_DeveAceitarValoresValidos(decimal teor)
        {
            // Arrange
            var lote = new LoteMinerio();

            // Act
            lote.TeorFe = teor;

            // Assert
            Assert.Equal(teor, lote.TeorFe);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(5.5)]
        [InlineData(8.2)]
        [InlineData(100)]
        public void Umidade_DeveAceitarValoresValidos(decimal umidade)
        {
            // Arrange
            var lote = new LoteMinerio();

            // Act
            lote.Umidade = umidade;

            // Assert
            Assert.Equal(umidade, lote.Umidade);
        }

        [Fact]
        public void SiO2_DeveSerOpcional()
        {
            // Arrange
            var lote = new LoteMinerio();

            // Assert
            Assert.Null(lote.SiO2);
        }

        [Fact]
        public void SiO2_DeveAceitarValorQuandoDefinido()
        {
            // Arrange
            var lote = new LoteMinerio();

            // Act
            lote.SiO2 = 2.5m;

            // Assert
            Assert.NotNull(lote.SiO2);
            Assert.Equal(2.5m, lote.SiO2.Value);
        }

        [Fact]
        public void P_DeveSerOpcional()
        {
            // Arrange
            var lote = new LoteMinerio();

            // Assert
            Assert.Null(lote.P);
        }

        [Fact]
        public void P_DeveAceitarValorQuandoDefinido()
        {
            // Arrange
            var lote = new LoteMinerio();

            // Acts
            lote.P = 0.045m;

            // Assert
            Assert.NotNull(lote.P);
            Assert.Equal(0.045m, lote.P.Value);
        }

        #endregion

        #region Testes de Logística

        [Theory]
        [InlineData(100)]
        [InlineData(1500.75)]
        [InlineData(50000)]
        public void Toneladas_DeveAceitarValoresPositivos(decimal toneladas)
        {
            // Arrange
            var lote = new LoteMinerio();

            // Act
            lote.Toneladas = toneladas;

            // Assert
            Assert.Equal(toneladas, lote.Toneladas);
        }

        [Fact]
        public void DataProducao_DeveAceitarDataValida()
        {
            // Arrange
            var lote = new LoteMinerio();
            var data = new DateTime(2026, 2, 10, 14, 30, 0);

            // Act
            lote.DataProducao = data;

            // Assert
            Assert.Equal(data, lote.DataProducao);
        }

        [Theory]
        [InlineData(StatusLote.EmEstoque)]
        [InlineData(StatusLote.EmTransporte)]
        [InlineData(StatusLote.Embarcado)]
        public void Status_DeveAceitarTodosOsValoresDoEnum(StatusLote status)
        {
            // Arrange
            var lote = new LoteMinerio();

            // Act
            lote.Status = status;

            // Assert
            Assert.Equal(status, lote.Status);
        }

        [Theory]
        [InlineData("Mina")]
        [InlineData("Pátio Carajás")]
        [InlineData("EFVM - Trem 123")]
        [InlineData("Porto Tubarão")]
        public void LocalizacaoAtual_DeveAceitarDiferentesLocalizacoes(string localizacao)
        {
            // Arrange
            var lote = new LoteMinerio();

            // Act
            lote.LocalizacaoAtual = localizacao;

            // Assert
            Assert.Equal(localizacao, lote.LocalizacaoAtual);
        }

        #endregion

        #region Testes de Código de Lote

        [Theory]
        [InlineData("MNA-2026-000001")]
        [InlineData("MNA-2026-000123")]
        [InlineData("MNA-2026-999999")]
        public void CodigoLote_DeveAceitarFormatoEsperado(string codigo)
        {
            // Arrange
            var lote = new LoteMinerio();

            // Act
            lote.CodigoLote = codigo;

            // Assert
            Assert.Equal(codigo, lote.CodigoLote);
        }

        [Theory]
        [InlineData("Carajás N4E")]
        [InlineData("Carajás S11D")]
        [InlineData("Brucutu")]
        [InlineData("Vargem Grande")]
        public void MinaOrigem_DeveAceitarDiferentesNomesDeMinas(string mina)
        {
            // Arrange
            var lote = new LoteMinerio();

            // Act
            lote.MinaOrigem = mina;

            // Assert
            Assert.Equal(mina, lote.MinaOrigem);
        }

        #endregion

        #region Testes de Cenários Completos

        [Fact]
        public void LoteMinerio_CenarioCompleto_LoteEmEstoque()
        {
            // Arrange & Act
            var lote = new LoteMinerio
            {
                Id = 1,
                CodigoLote = "MNA-2026-000001",
                MinaOrigem = "Carajás N4E",
                TeorFe = 67.5m,
                Umidade = 8.0m,
                SiO2 = 1.8m,
                P = 0.04m,
                Toneladas = 2000m,
                DataProducao = new DateTime(2026, 2, 1),
                Status = StatusLote.EmEstoque,
                LocalizacaoAtual = "Pátio Carajás"
            };

            // Assert
            Assert.Equal(StatusLote.EmEstoque, lote.Status);
            Assert.Equal("Pátio Carajás", lote.LocalizacaoAtual);
            Assert.True(lote.TeorFe > 65); // Alta qualidade
        }

        [Fact]
        public void LoteMinerio_CenarioCompleto_LoteEmTransporte()
        {
            // Arrange & Act
            var lote = new LoteMinerio
            {
                Id = 2,
                CodigoLote = "MNA-2026-000002",
                MinaOrigem = "Carajás S11D",
                TeorFe = 66.2m,
                Umidade = 7.5m,
                Toneladas = 1800m,
                DataProducao = new DateTime(2026, 2, 5),
                Status = StatusLote.EmTransporte,
                LocalizacaoAtual = "EFVM - Trem 456"
            };

            // Assert
            Assert.Equal(StatusLote.EmTransporte, lote.Status);
            Assert.Contains("EFVM", lote.LocalizacaoAtual);
        }

        [Fact]
        public void LoteMinerio_CenarioCompleto_LoteEmbarcado()
        {
            // Arrange & Act
            var lote = new LoteMinerio
            {
                Id = 3,
                CodigoLote = "MNA-2026-000003",
                MinaOrigem = "Brucutu",
                TeorFe = 65.8m,
                Umidade = 9.0m,
                SiO2 = 2.2m,
                Toneladas = 2500m,
                DataProducao = new DateTime(2026, 1, 28),
                Status = StatusLote.Embarcado,
                LocalizacaoAtual = "Porto Tubarão - Navio MV Iron Star"
            };

            // Assert
            Assert.Equal(StatusLote.Embarcado, lote.Status);
            Assert.Contains("Porto", lote.LocalizacaoAtual);
        }

        #endregion
    }

    public class StatusLoteTests
    {
        [Fact]
        public void StatusLote_DeveConter3Valores()
        {
            // Arrange & Act
            var valores = Enum.GetValues(typeof(StatusLote));

            // Assert
            Assert.Equal(3, valores.Length);
        }

        [Theory]
        [InlineData(StatusLote.EmEstoque, 0)]
        [InlineData(StatusLote.EmTransporte, 1)]
        [InlineData(StatusLote.Embarcado, 2)]
        public void StatusLote_DeveRetornarValorCorreto(StatusLote status, int valorEsperado)
        {
            // Assert
            Assert.Equal(valorEsperado, (int)status);
        }

        [Fact]
        public void StatusLote_DeveConterTodosOsStatus()
        {
            // Arrange
            var statusEsperados = new[] 
            { 
                StatusLote.EmEstoque, 
                StatusLote.EmTransporte, 
                StatusLote.Embarcado 
            };

            // Act
            var statusDefinidos = (StatusLote[])Enum.GetValues(typeof(StatusLote));

            // Assert
            Assert.Equal(statusEsperados.Length, statusDefinidos.Length);
            Assert.Contains(StatusLote.EmEstoque, statusDefinidos);
            Assert.Contains(StatusLote.EmTransporte, statusDefinidos);
            Assert.Contains(StatusLote.Embarcado, statusDefinidos);
        }
    }
}