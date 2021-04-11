using Confluent.Kafka;
using EqServer.EqModels.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EqServer.DL.Kafka.Producers
{
    public class CalculationPackProducer : ICalculationPackProducer
    {
        private readonly ILogger<CalculationPackProducer> _logger;
        private readonly IProducer<int, CalculationPack> _producer;

        public CalculationPackProducer(ILogger<CalculationPackProducer> logger)
        {
            _logger = logger;

            var config = new ProducerConfig()
            {
                BootstrapServers = "localhost:9092"
            };

            _producer = new ProducerBuilder<int, CalculationPack>(config)
                .SetValueSerializer(new MsgPackSerializer<CalculationPack>())
                .Build();


        }

        public async Task GeneratePacks(List<CalculationPack> data)
        {
            foreach (var item in data)
            {
                var msg = new Message<int, CalculationPack>()
                {
                    Key = item.Id,
                    Value = item
                };

                await _producer.ProduceAsync("data", msg);
            }
        }
    }
}
