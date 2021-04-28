using EqServer.EqModels.Models;
using System.Threading.Tasks;

namespace EqServer.DL.Interfaces
{
    public interface ICalculationPackRepository
    {
        Task<CalculationPack> Create(CalculationPack calc);

        Task<CalculationPack> Get(int id);
    }
}
