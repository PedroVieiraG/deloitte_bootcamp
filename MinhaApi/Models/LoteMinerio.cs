namespace MinhaApi.Models
{
    public enum StatusLote { EmEstoque = 0, EmTransporte = 1, Embarcado = 2 }
    public enum QualidadeLote { Premium, Padrao, Baixa }

    public class LoteMinerio
    {
        // Propriedades com Private Set para garantir o encapsulamento
        public int Id { get; private set; }
        public string CodigoLote { get; init; } = "";
        public string MinaOrigem { get; init; } = "";
        public decimal TeorFe { get; init; }
        public decimal Umidade { get; init; }
        public decimal? SiO2 { get; init; }
        public decimal? P { get; init; }
        public decimal Toneladas { get; init; }
        public DateTime DataProducao { get; init; }
        public StatusLote Status { get; private set; }
        public string LocalizacaoAtual { get; private set; } = "";
        public List<HistoricoMovimentacao> Historico { get; private set; } = new();

        // --- Regras de Negócio Internas (Read-only Properties) ---

        public QualidadeLote Qualidade => (TeorFe, Umidade, SiO2 ?? 0) switch
        {
            (>= 65, <= 6, <= 3) => QualidadeLote.Premium,
            (>= 62, <= 8, _)     => QualidadeLote.Padrao,
            _                   => QualidadeLote.Baixa
        };

        public decimal PrecoPorTonelada => Qualidade switch
        {
            QualidadeLote.Premium => 185.50m,
            QualidadeLote.Padrao  => 156.90m,
            _                     => 98.00m
        };

        // --- Métodos de Comportamento ---

        public decimal CalcularValorLiquido()
        {
            decimal penalidadeUmidade = Umidade > 8 ? (Umidade - 8) * 1.50m * Toneladas : 0m;
            return (PrecoPorTonelada * Toneladas) - penalidadeUmidade;
        }

        public void RegistrarMovimentacao(StatusLote novoStatus, string novoLocal)
        {
            // Validação de transição de status
            if (Status == StatusLote.Embarcado)
                throw new InvalidOperationException("Não é possível movimentar um lote já embarcado.");

            Status = novoStatus;
            LocalizacaoAtual = novoLocal;

            Historico.Add(new HistoricoMovimentacao
            {
                Status = novoStatus,
                Local = novoLocal,
                Data = DateTime.UtcNow
            });
        }
    }
}