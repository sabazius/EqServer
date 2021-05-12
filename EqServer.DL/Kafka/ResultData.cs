using EqServer.EqModels.Models;
using System.Collections.Concurrent;

namespace EqServer.DL.Kafka
{
    public static class ResultData
    {
        public static ConcurrentQueue<CalculationPack> _result = new ConcurrentQueue<CalculationPack>();

    }
}
