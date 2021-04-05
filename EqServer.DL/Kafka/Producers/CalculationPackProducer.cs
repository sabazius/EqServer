using Confluent.Kafka;
using EqServer.EqModels.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace EqServer.DL.Kafka.Producers
{
    public class CalculationPackProducer : IHostedService
    {
        private readonly ILogger<CalculationPackProducer> _logger;
        private readonly IProducer<int, Equation> _producer;

        public CalculationPackProducer(ILogger<CalculationPackProducer> logger)
        {
            _logger = logger;

            var config = new ProducerConfig()
            {
                BootstrapServers = "localhost:9092"
            };

            _producer = new ProducerBuilder<int, Equation>(config)
                .SetValueSerializer(new MsgPackSerializer<Equation>())
                .Build();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {

            var msg = new Message<int, Equation>() 
            {
              Key = 1,
              Value = new Equation
              {
                Id = 1,
                EqMethod = "ax + b = c"
              }
            };

            await _producer.ProduceAsync("data", msg);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _producer.Dispose();
            return Task.CompletedTask;
        }
    }
}
