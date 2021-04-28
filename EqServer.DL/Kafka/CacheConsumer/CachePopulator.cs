using Confluent.Kafka;
using EqServer.DL.Kafka.Producers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace EqServer.DL.Kafka.CacheConsumer
{
    public class CachePopulator<TKey, TValue> : IHostedService
    {
        private readonly ILogger<CalculationPackProducer> _logger;
        private readonly IProducer<TKey, TValue> _producer;

        public CachePopulator(ILogger<CalculationPackProducer> logger)
        {
            var config = new ProducerConfig()
            {
                BootstrapServers = "localhost:9092",
            };

            _producer = new ProducerBuilder<TKey, TValue>(config)
                .SetValueSerializer(new MsgPackSerializer<TValue>())
                .Build();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
