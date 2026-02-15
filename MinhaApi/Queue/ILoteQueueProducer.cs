using System.Text.Json;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace MinhaApi.Queue
{
    public interface ILoteQueueProducer
    {
        Task EnfileirarAsync(ProcessarLoteMessage msg, CancellationToken ct = default);
    }

    public class LoteQueueProducer : ILoteQueueProducer
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly RedisQueueOptions _opts;
        private static readonly JsonSerializerOptions _json = new(JsonSerializerDefaults.Web);

        public LoteQueueProducer(IConnectionMultiplexer redis, IOptions<RedisQueueOptions> opts)
        {
            _redis = redis;
            _opts = opts.Value;
        }

        public async Task EnfileirarAsync(ProcessarLoteMessage msg, CancellationToken ct = default)
        {
            var db = _redis.GetDatabase();
            var payload = JsonSerializer.Serialize(msg, _json);

            var entries = new NameValueEntry[]
            {
                new("type", msg.Acao),
                new("payload", payload)
            };

            await db.StreamAddAsync(_opts.StreamName, entries);
        }
    }
}