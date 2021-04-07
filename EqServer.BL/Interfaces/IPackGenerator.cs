using EqServer.EqModels.Models;
using System.Threading.Tasks;

namespace EqServer.BL.Interfaces
{
    public interface IPackGenerator
    {
        Task<int> GeneratePacks(int numOfCalcs, int numOfUnits);
    }
}
