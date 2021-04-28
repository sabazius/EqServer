using EqServer.EqModels.Models;
using System.Threading.Tasks;

namespace EqServer.BL.Interfaces
{
    public interface ICalculationPackService
    {
        Task<CalculationPack> Create(CalculationPack calc);

        Task<CalculationPack> GetById(int id);
    }
}
