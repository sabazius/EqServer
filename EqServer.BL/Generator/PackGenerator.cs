using EqServer.BL.Interfaces;
using EqServer.DL.Kafka;
using EqServer.DL.Kafka.Producers;
using EqServer.EqModels.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EqServer.BL.Generator
{
    public class PackGenerator : IPackGenerator
    {
        private readonly ICalculationPackProducer _calculationPackProducer;
        private readonly KafkaAdmin _kafkaAdmin;

        public PackGenerator(ICalculationPackProducer calculationPackProducer, KafkaAdmin kafkaAdmin)
        {
            _calculationPackProducer = calculationPackProducer;
            _kafkaAdmin = kafkaAdmin;
        }

        public async Task<bool> DeleteCalcTopic(string topicName)
        {
            return await _kafkaAdmin.DeleteCalcTopic(topicName);
        }

        public async Task<int> GeneratePacks(int numOfCalcs, int numOfUnits)
        {
            var result = new List<CalculationPack>();

            for (int i = 0; i < numOfCalcs; i++)
            {
                Random rand = new Random(i);
                var id = rand.Next();

                var pack = new CalculationPack()
                {
                    Id = id,
                    Count = numOfCalcs,
                    EqId = 1,
                    Data = GenerateUnits(numOfUnits, id)
                };
                result.Add(pack);
            }

            await _calculationPackProducer.GeneratePacks(result);

            return numOfCalcs;
        }

        private List<CalculationUnit> GenerateUnits(int numOfUnits, int calcPackId)
        {
            var result = new List<CalculationUnit>();
            for (int i = 0; i < numOfUnits; i++)
            {
                Random rand = new Random(i);

                result.Add(new CalculationUnit
                {
                    CalcPackId = calcPackId,
                    Number = i,
                    Equation = new Equation
                    {
                        Id = i,
                        EqMethod = rand.Next(0, 2345).ToString() + "*" + rand.Next(123, 567).ToString() + "+" + rand.Next(0, 10000).ToString(),
                        Result = -1
                    }

                });
            }

            return result;
        }
    }
}
