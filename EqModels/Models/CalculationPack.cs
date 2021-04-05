using System.Collections.Generic;

namespace EqServer.EqModels.Models
{
    public class CalculationPack
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public int EqId { get; set; }
        public List<CalculationUnit> Data { get; set; }
    }
}