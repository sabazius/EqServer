using EqServer.BL.Interfaces;
using EqServer.DL.Interfaces;
using EqServer.EqModels.Models;
using System.Threading.Tasks;

namespace EqServer.BL.Services
{
    public class CalculationPackService : ICalculationPackService
    {
        private readonly ICalculationPackRepository _calculationPackRepository;

        public CalculationPackService(ICalculationPackRepository calculationPackRepository)
        {
            _calculationPackRepository = calculationPackRepository;
        }

        public async Task<CalculationPack> Create(CalculationPack calc)
        {
            return await _calculationPackRepository.Create(calc);
        }

        public async Task<CalculationPack> GetById(int id)
        {
            return await _calculationPackRepository.Get(id);
        }
    }
}
