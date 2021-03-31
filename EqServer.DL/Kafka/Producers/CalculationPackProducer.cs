using Confluent.Kafka;
using EqModels.Models;
using MessagePack;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MsgPack.Serialization;
using System;
using System.Collections.Generic;
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
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    
}
