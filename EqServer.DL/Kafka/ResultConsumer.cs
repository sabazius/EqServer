using Confluent.Kafka;
using EqServer.DL.Kafka;
using EqServer.EqModels.Models;
using MessagePack;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using EqServer.DL.Mongo;

namespace EqServer.DataLayer.Kafka
{
    public class ResultConsumer : BackgroundService
    {

        private readonly ILogger<ResultConsumer> _logger;
        private readonly ConsumerConfig _kafkaConfig;
        private readonly IResultsRepository _resultsRepository;
        public ResultConsumer(ILogger<ResultConsumer> logger, IResultsRepository resultsRepository)
        {
            _logger = logger;
            _resultsRepository = resultsRepository;

            _kafkaConfig = new ConsumerConfig
            {
                EnableAutoCommit = true,
                AutoCommitIntervalMs = 5000,
                FetchWaitMaxMs = 50,
                BootstrapServers = "localhost:9092",
                GroupId = $"EqClient",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() =>
            {
                using var consumer = new ConsumerBuilder<int, byte[]>(_kafkaConfig)
                    .Build();

                try
                {
                    consumer.Subscribe("results");

                    while (!stoppingToken.IsCancellationRequested)
                    {
                        try
                        {
                            var consumeResult = consumer.Consume(stoppingToken);

                            try
                            {
                                //_calculationDataFlow.ProcessMessage(consumeResult.Message.Value);
                                var deserializedMessage = MessagePackSerializer.Deserialize<CalculationPack>(consumeResult.Message.Value);
                                ResultData._result.Enqueue(deserializedMessage);

                                _resultsRepository.Save(deserializedMessage);

                                _logger.LogInformation(consumeResult.Message.Key.ToString());
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError(ex, "Kafka consume message error: {0}", ex.Message);
                            }

                            if (consumeResult.IsPartitionEOF)
                                break;

                        }
                        catch (ConsumeException e)
                        {
                            _logger.LogError(e, $"Consumer for topic '{e.ConsumerRecord.Topic}'. ConsumeException, Key: {e.Error}");
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Kafka consume message error: {0}", ex.Message);
                        }
                    }
                }
                catch (OperationCanceledException e)
                {
                    _logger.LogError(e, $"Consumer for topics data: {e.Message}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Consumer for topics '{string.Join(';', "data")}'. Exception.");
                }
            }, stoppingToken);
        }
    }
}
