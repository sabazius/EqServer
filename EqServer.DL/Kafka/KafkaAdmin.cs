using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EqServer.DL.Kafka
{
    public class KafkaAdmin
    {
        private readonly AdminClientBuilder _adminBuilder;
        private readonly IAdminClient _adminClient;

        public KafkaAdmin()
        {
            var config = new AdminClientConfig()
            {
                BootstrapServers = "localhost:9092",
                
            };

            _adminBuilder = new AdminClientBuilder(config);

            _adminClient = _adminBuilder.Build();
        }


        public async Task<bool> DeleteCalcTopic()
        {
            var topics = new List<string>();

            topics.Add("xxx");

            await _adminClient.DeleteTopicsAsync(topics.AsEnumerable());

            return true;
        }

    }
}
