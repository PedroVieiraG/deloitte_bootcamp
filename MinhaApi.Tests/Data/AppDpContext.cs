// using DotNet.Testcontainers.Builders;
// using Microsoft.EntityFrameworkCore;
// using MinhaApi.Data;
// using MinhaApi.Models;
// using Testcontainers.PostgreSql;
// using Xunit;

// namespace MinhaApi.Tests.Data
// {
//     public class PostgresIntegrationTests : IAsyncLifetime
//     {
//         // Define o container do Postgres
//         private readonly PostgreSqlContainer _postgresContainer = new PostgreSqlBuilder()
//             .WithImage("postgres:15-alphabetical") // Versão que preferir
//             .WithDatabase("db_mineracao")
//             .WithUsername("admin")
//             .WithPassword("senha123")
//             .Build();

//         // Inicializa o container antes dos testes
//         public async Task InitializeAsync() => await _postgresContainer.StartAsync();

//         // Mata o container após os testes
//         public async Task DisposeAsync() => await _postgresContainer.DisposeAsync().AsTask();

//         private AppDbContext GetDbContext()
//         {
//             var options = new DbContextOptionsBuilder<AppDbContext>()
//                 .UseNpgsql(_postgresContainer.GetConnectionString())
//                 .Options;

//             var context = new AppDbContext(options);
//             context.Database.EnsureCreated(); // Cria as tabelas e o Schema "public"
//             return context;
//         }

//         [Fact]
//         public async Task DevePersistirDadosComTiposNumericosPrecisos()
//         {
//             // Arrange
//             using var context = GetDbContext();
//             var lote = new LoteMinerio
//             {
//                 CodigoLote = "MNA-POSTGRES-001",
//                 MinaOrigem = "Carajás",
//                 LocalizacaoAtual = "Porto",
//                 Toneladas = 1250.555m, // Valida o numeric(12,3)
//                 TeorFe = 65.12m        // Valida o numeric(5,2)
//             };

//             // Act
//             context.LotesMinerio.Add(lote);
//             await context.SaveChangesAsync();

//             // Assert
//             var salvo = await context.LotesMinerio.FirstAsync();
//             Assert.Equal(1250.555m, salvo.Toneladas);
//             Assert.Equal(65.12m, salvo.TeorFe);
//         }

//         [Fact]
//         public async Task DeveRespeitarSchemaEConstraintsDoPostgres()
//         {
//             using var context = GetDbContext();
            
//             // Tenta inserir duplicado para testar o índice único real do Postgres
//             var lote1 = new LoteMinerio { CodigoLote = "IGUAL", MinaOrigem = "A", LocalizacaoAtual = "X" };
//             var lote2 = new LoteMinerio { CodigoLote = "IGUAL", MinaOrigem = "B", LocalizacaoAtual = "Y" };

//             context.LotesMinerio.Add(lote1);
//             await context.SaveChangesAsync();

//             context.LotesMinerio.Add(lote2);
            
//             // O Postgres lançará uma PostgresException de violação de Unique Key
//             await Assert.ThrowsAsync<DbUpdateException>(() => context.SaveChangesAsync());
//         }
//     }
// }