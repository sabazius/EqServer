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

        public CalculationUnit GenerateCalcUnit()
        {
            throw new NotImplementedException();
        }

        public async Task<int> GeneratePacks(int numOfCalcs)
        {
            var result = new List<CalculationPack>();

            for (int i = 0; i < numOfCalcs; i++)
            {
                var pack = new CalculationPack()
                {
                    Id = i,
                    Count = numOfCalcs,
                    EqId = 1,
                    Data = GenerateUnits(50, 10, new Equation
                    {
                        Id = i + 3,
                        EqMethod = "ax + b = c"
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
