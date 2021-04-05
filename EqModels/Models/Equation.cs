using MessagePack;

namespace EqServer.EqModels.Models
{
    [MessagePackObject]
    public class Equation
    {
        [Key(0)]
        public int Id { get; set; }

        [Key(1)]
        public string EqMethod { get; set; }
    }
}
