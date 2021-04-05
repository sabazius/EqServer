namespace EqServer.EqModels.Models
{
    public class CalculationUnit
    {
        public int Number { get; set; }
        public int CalcPackId { get; set; }
        public Equation Equation { get; set; }
    }
}