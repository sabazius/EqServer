using EqServer.EqModels.Models;

namespace EqServer.BL.Interfaces
{
    public interface IPackGenerator
    {
        int GeneratePack(int numOfCalcs);

        CalculationUnit GenerateCalcUnit();
    }
}
