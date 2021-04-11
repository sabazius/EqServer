using EqServer.EqModels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EqServer.DL.Kafka.Producers
{
    public interface ICalculationPackProducer
    {
        Task GeneratePacks(List<CalculationPack> data);
    }
}