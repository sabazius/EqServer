using Confluent.Kafka;
using EqModels.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EqServer.DL.Kafka.Producers
{
    public class CalculationPackProducer : IHostedService
    {
        private readonly ILogger<CalculationPackProducer> _logger;
        private readonly IProducer<int, CalculationPackProducer> _producer;

        public CalculationPackProducer(ILogger<CalculationPackProducer> logger)
        {
            _logger = logger;

            var config = new ProducerConfig()
            {
                BootstrapServers = "localhost:9092"
            };

            _producer = new ProducerBuilder<int, CalculationPackProducer>(config).Build();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {

            var msg = new Message<int, CalculationPackProducer>() 
            {
                
            };




            _producer.ProduceAsync("testtopic")

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    
}
