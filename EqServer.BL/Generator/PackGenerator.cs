using EqServer.BL.Interfaces;
using EqServer.EqModels.Models;
using System;
using System.Collections.Generic;

namespace EqServer.BL.Generator
{
    public class PackGenerator : IPackGenerator
    {

        public PackGenerator()
        {

        }

        public CalculationUnit GenerateCalcUnit()
        {
            throw new NotImplementedException();
        }

        public int GeneratePack(int numOfCalcs)
        {
            for (int i = 0; i < numOfCalcs; i++)
            {
                var pack = new CalculationPack()
                {
                    Id = i,
                    Count = numOfCalcs,
                    EqId = 1,
                    Data = GenerateUnits(50, 10, null)
                };
            }
            return 0;
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
