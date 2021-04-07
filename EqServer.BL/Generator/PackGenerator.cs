using EqServer.BL.Interfaces;
using EqServer.DL.Kafka.Producers;
using EqServer.EqModels.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EqServer.BL.Generator
{
    public class PackGenerator : IPackGenerator
    {
        ICalculationPackProducer _calculationPackProducer;

        public PackGenerator(ICalculationPackProducer calculationPackProducer)
        {
            _calculationPackProducer = calculationPackProducer;
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
                    Data = GenerateUnits(numOfUnits, id, new Equation
                    {
                        Id = i + 3,
                        EqMethod = rand.Next(0, 2345).ToString() + rand.Next(123, 567).ToString() + "+" + rand.Next(0, 10000).ToString() //"ax + b = c"
                    })
                };
                result.Add(pack);
            }

            await _calculationPackProducer.GeneratePacks(result);

            return numOfCalcs;
        }

        private List<CalculationUnit> GenerateUnits(int numOfUnits, int calcPackId, Equation eq)
        {
            var result = new List<CalculationUnit>();
            for (int i = 0; i < numOfUnits; i++)
            {
                result.Add(new CalculationUnit
                {
                    CalcPackId = calcPackId,
                    Number = i,
                    Equation = eq
                });
            }

            return result;
        }
    }
}
