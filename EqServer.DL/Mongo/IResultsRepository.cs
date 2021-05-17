using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using EqServer.EqModels.Models;

namespace EqServer.DL.Mongo
{
    public interface IResultsRepository
    {
        Task<CalculationPack> Save(CalculationPack pack);

        Task<IEnumerable<CalculationPack>> GetAll();
    }
}
