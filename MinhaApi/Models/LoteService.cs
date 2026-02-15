using MinhaApi.Models;

namespace MinhaApi.Services
{
    public class LoteService
    {
        /// <summary>
        /// Avança o status do lote seguindo a regra de negócio logística.
        /// </summary>
        public void AvancarStatus(LoteMinerio lote)
        {
            // Definimos qual é o próximo passo lógico baseado no status atual
            var proximoStatus = lote.Status switch
            {
                StatusLote.EmEstoque    => StatusLote.EmTransporte,
                StatusLote.EmTransporte => StatusLote.Embarcado,
                StatusLote.Embarcado    => throw new InvalidOperationException("O lote já foi embarcado e não pode mais avançar."),
                _                       => throw new ArgumentOutOfRangeException(nameof(lote.Status), "Status desconhecido.")
            };

            // O próprio modelo se encarrega de atualizar o status, 
            // a localização e gravar no histórico simultaneamente.
            lote.RegistrarMovimentacao(proximoStatus, lote.LocalizacaoAtual);
        }

        /// <summary>
        /// Retorna um resumo financeiro do lote (útil para relatórios ou faturamento).
        /// </summary>
        public object ObterResumoFinanceiro(LoteMinerio lote)
        {
            return new
            {
                lote.CodigoLote,
                lote.Toneladas,
                lote.Qualidade,
                PrecoBase = lote.PrecoPorTonelada,
                ValorLiquidoTotal = lote.CalcularValorLiquido(),
                DataProcessamento = DateTime.UtcNow
            };
        }

        // Aqui você poderia adicionar métodos que interagem com o Banco de Dados
        // public void Salvar(LoteMinerio lote) => _repository.Update(lote);
    }
}