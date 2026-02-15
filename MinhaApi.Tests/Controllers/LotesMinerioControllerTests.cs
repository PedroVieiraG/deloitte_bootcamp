using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhaApi.Controllers;
using MinhaApi.Data;
using MinhaApi.Models;
using MinhaApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaApi.Tests.Controllers
{
    public class LotesMinerioControllerTests : IDisposable
    {
        private readonly AppDbContext _context;
        private readonly LotesMinerioController _controller;

        public LotesMinerioControllerTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _controller = new LotesMinerioController(_context);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        #region Helper Methods

        private CreateLoteMinerioDto CriarDtoValido(string codigo = "MNA-2026-000001")
        {
            return new CreateLoteMinerioDto
            {
                CodigoLote = codigo,
                MinaOrigem = "Carajás N4E",
                TeorFe = 67.5m,
                Umidade = 8.0m,
                Toneladas = 2000m,
                Status = 0,
                LocalizacaoAtual = "Pátio"
            };
        }

        private async Task<LoteMinerio> CriarLoteNoBanco(string codigo = "MNA-2026-000001")
        {
            var lote = new LoteMinerio
            {
                CodigoLote = codigo,
                MinaOrigem = "Carajás N4E",
                TeorFe = 67.5m,
                Umidade = 8.0m,
                Toneladas = 2000m,
                DataProducao = DateTime.UtcNow,
                Status = StatusLote.EmEstoque,
                LocalizacaoAtual = "Pátio"
            };
            _context.LotesMinerio.Add(lote);
            await _context.SaveChangesAsync();
            return lote;
        }

        #endregion

        #region Testes de Create

        [Fact]
        public async Task Create_ComDadosValidos_DeveRetornarCreatedAtAction()
        {
            // Arrange
            var dto = CriarDtoValido();

            // Act
            var result = await _controller.Create(dto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var lote = Assert.IsType<LoteMinerio>(createdResult.Value);
            Assert.Equal(dto.CodigoLote, lote.CodigoLote);
        }

        [Theory]
        [InlineData("", "MinaOrigem", 67.5, 8.0, 2000, 0, "Pátio", "CodigoLote é obrigatório.")]
        [InlineData("MNA-001", "", 67.5, 8.0, 2000, 0, "Pátio", "MinaOrigem é obrigatória.")]
        [InlineData("MNA-001", "Mina", 67.5, 8.0, 2000, 0, "", "LocalizacaoAtual é obrigatória.")]
        [InlineData("MNA-001", "Mina", -1, 8.0, 2000, 0, "Pátio", "TeorFe deve estar entre 0 e 100 (%).")]
        [InlineData("MNA-001", "Mina", 101, 8.0, 2000, 0, "Pátio", "TeorFe deve estar entre 0 e 100 (%).")]
        [InlineData("MNA-001", "Mina", 67.5, -1, 2000, 0, "Pátio", "Umidade deve estar entre 0 e 100 (%).")]
        [InlineData("MNA-001", "Mina", 67.5, 101, 2000, 0, "Pátio", "Umidade deve estar entre 0 e 100 (%).")]
        [InlineData("MNA-001", "Mina", 67.5, 8.0, 0, 0, "Pátio", "Toneladas deve ser > 0.")]
        [InlineData("MNA-001", "Mina", 67.5, 8.0, -100, 0, "Pátio", "Toneladas deve ser > 0.")]
        [InlineData("MNA-001", "Mina", 67.5, 8.0, 2000, -1, "Pátio", "Status inválido (use 0, 1 ou 2).")]
        [InlineData("MNA-001", "Mina", 67.5, 8.0, 2000, 3, "Pátio", "Status inválido (use 0, 1 ou 2).")]
        public async Task Create_ComDadosInvalidos_DeveRetornarBadRequest(
            string codigo, string mina, decimal teor, decimal umidade, 
            decimal toneladas, int status, string localizacao, string mensagemEsperada)
        {
            // Arrange
            var dto = new CreateLoteMinerioDto
            {
                CodigoLote = codigo,
                MinaOrigem = mina,
                TeorFe = teor,
                Umidade = umidade,
                Toneladas = toneladas,
                Status = status,
                LocalizacaoAtual = localizacao
            };

            // Act
            var result = await _controller.Create(dto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(mensagemEsperada, badRequestResult.Value);
        }

        [Fact]
        public async Task Create_ComCodigoLoteDuplicado_DeveRetornarConflict()
        {
            // Arrange
            await CriarLoteNoBanco("MNA-2026-000001");
            var dto = CriarDtoValido("MNA-2026-000001");

            // Act
            var result = await _controller.Create(dto);

            // Assert
            var conflictResult = Assert.IsType<ConflictObjectResult>(result);
            Assert.Contains("MNA-2026-000001", conflictResult.Value?.ToString());
        }

        [Fact]
        public async Task Create_SemDataProducao_DeveUsarDataAtual()
        {
            // Arrange
            var antes = DateTime.UtcNow;
            var dto = CriarDtoValido();
            dto.DataProducao = null;

            // Act
            var result = await _controller.Create(dto);
            var depois = DateTime.UtcNow;

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var lote = Assert.IsType<LoteMinerio>(createdResult.Value);
            Assert.True(lote.DataProducao >= antes && lote.DataProducao <= depois);
        }

        #endregion

        #region Testes de GetById e GetLote

        [Fact]
        public async Task GetById_ComIdExistente_DeveRetornarOk()
        {
            // Arrange
            var lote = await CriarLoteNoBanco();

            // Act
            var result = await _controller.GetById(lote.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var loteRetornado = Assert.IsType<LoteMinerio>(okResult.Value);
            Assert.Equal(lote.Id, loteRetornado.Id);
        }

        [Fact]
        public async Task GetById_ComIdInexistente_DeveRetornarNotFound()
        {
            // Act
            var result = await _controller.GetById(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetLote_ComIdExistente_DeveRetornarDto()
        {
            // Arrange
            var lote = await CriarLoteNoBanco();

            // Act
            var result = await _controller.GetLote(lote.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var dto = Assert.IsType<LoteMinerioResponseDto>(okResult.Value);
            Assert.Equal(lote.Id, dto.Id);
            Assert.Equal(lote.CodigoLote, dto.CodigoLote);
        }

        [Fact]
        public async Task GetLote_ComIdInexistente_DeveRetornarNotFound()
        {
            // Act
            var result = await _controller.GetLote(999);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        #endregion

        #region Testes de GetAll

        [Fact]
        public async Task GetAll_ComLotesNoBanco_DeveRetornarLista()
        {
            // Arrange
            await CriarLoteNoBanco("MNA-001");
            await CriarLoteNoBanco("MNA-002");

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var lotes = Assert.IsAssignableFrom<IEnumerable<LoteMinerioResponseDto>>(okResult.Value);
            Assert.Equal(2, lotes.Count());
        }

        [Fact]
        public async Task GetAll_SemLotes_DeveRetornarListaVazia()
        {
            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var lotes = Assert.IsAssignableFrom<IEnumerable<LoteMinerioResponseDto>>(okResult.Value);
            Assert.Empty(lotes);
        }

        #endregion

        #region Testes de Update

        [Fact]
        public async Task Update_ComDadosValidos_DeveAtualizarERetornarNoContent()
        {
            // Arrange
            var lote = await CriarLoteNoBanco();
            var dto = CriarDtoValido("MNA-UPDATED");
            dto.TeorFe = 68.0m;
            dto.Status = 1;

            // Act
            var result = await _controller.Update(lote.Id, dto);

            // Assert
            Assert.IsType<NoContentResult>(result);
            
            var loteAtualizado = await _context.LotesMinerio.FindAsync(lote.Id);
            Assert.Equal("MNA-UPDATED", loteAtualizado!.CodigoLote);
            Assert.Equal(68.0m, loteAtualizado.TeorFe);
            Assert.Equal(StatusLote.EmTransporte, loteAtualizado.Status);
        }

        [Fact]
        public async Task Update_ComIdInexistente_DeveRetornarNotFound()
        {
            // Arrange
            var dto = CriarDtoValido();

            // Act
            var result = await _controller.Update(999, dto);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Update_ComCodigoLoteDuplicado_DeveRetornarConflict()
        {
            // Arrange
            await CriarLoteNoBanco("MNA-001");
            var lote2 = await CriarLoteNoBanco("MNA-002");
            
            var dto = CriarDtoValido("MNA-001"); // Tentando usar código do lote1

            // Act
            var result = await _controller.Update(lote2.Id, dto);

            // Assert
            var conflictResult = Assert.IsType<ConflictObjectResult>(result);
            Assert.Contains("MNA-001", conflictResult.Value?.ToString());
        }

        [Fact]
        public async Task Update_SemDataProducao_DeveManterDataOriginal()
        {
            // Arrange
            var dataOriginal = new DateTime(2026, 1, 1);
            var lote = await CriarLoteNoBanco();
            lote.DataProducao = dataOriginal;
            await _context.SaveChangesAsync();

            var dto = CriarDtoValido();
            dto.DataProducao = null;

            // Act
            await _controller.Update(lote.Id, dto);

            // Assert
            var loteAtualizado = await _context.LotesMinerio.FindAsync(lote.Id);
            Assert.Equal(dataOriginal, loteAtualizado!.DataProducao);
        }

        #endregion

        #region Testes de Delete

        [Fact]
        public async Task Delete_ComIdExistente_DeveRemoverERetornarNoContent()
        {
            // Arrange
            var lote = await CriarLoteNoBanco();

            // Act
            var result = await _controller.Delete(lote.Id);

            // Assert
            Assert.IsType<NoContentResult>(result);
            
            var loteRemovido = await _context.LotesMinerio.FindAsync(lote.Id);
            Assert.Null(loteRemovido);
        }

        [Fact]
        public async Task Delete_ComIdInexistente_DeveRetornarNotFound()
        {
            // Act
            var result = await _controller.Delete(999);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        #endregion

        #region Teste de Integração

        [Fact]
        public async Task CicloCompleto_CRUD_DeveFuncionarCorretamente()
        {
            // 1. Create
            var createDto = CriarDtoValido();
            var createResult = await _controller.Create(createDto);
            var createdResult = Assert.IsType<CreatedAtActionResult>(createResult);
            var lote = Assert.IsType<LoteMinerio>(createdResult.Value);

            // 2. GetAll - deve conter 1 lote
            var getAllResult = await _controller.GetAll();
            var okGetAll = Assert.IsType<OkObjectResult>(getAllResult.Result);
            var lotes = Assert.IsAssignableFrom<IEnumerable<LoteMinerioResponseDto>>(okGetAll.Value);
            Assert.Single(lotes);

            // 3. Update
            var updateDto = CriarDtoValido("MNA-UPDATED");
            var updateResult = await _controller.Update(lote.Id, updateDto);
            Assert.IsType<NoContentResult>(updateResult);

            // 4. Delete
            var deleteResult = await _controller.Delete(lote.Id);
            Assert.IsType<NoContentResult>(deleteResult);

            // 5. Verificar que não existe mais
            var getAfterDelete = await _controller.GetLote(lote.Id);
            Assert.IsType<NotFoundObjectResult>(getAfterDelete.Result);
        }

        #endregion
    }
}