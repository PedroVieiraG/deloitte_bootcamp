namespace MinhaApi.Models
{
    public class HistoricoMovimentacao
    {
        public DateTime Data { get; set; } = DateTime.UtcNow;
        public string Local { get; set; } = "";
        public StatusLote Status { get; set; }
    }
}