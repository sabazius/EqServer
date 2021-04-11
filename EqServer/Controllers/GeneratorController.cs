using EqServer.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace EqServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeneratorController : ControllerBase
    {

        private readonly ILogger<GeneratorController> _logger;
        private readonly IPackGenerator _packGenerator;

        public GeneratorController(ILogger<GeneratorController> logger, IPackGenerator packGenerator)
        {
            _logger = logger;
            _packGenerator = packGenerator;
        }

        [HttpPost(nameof(CreateCalculationPacks))]
        public IActionResult CreateCalculationPacks(int numOfPacks, int numOfUnits)
        {
            if (numOfPacks <= 0)
            {
                return BadRequest(numOfPacks);
            }

            _packGenerator.GeneratePacks(numOfPacks, numOfUnits);

            return Ok(numOfPacks);
        }

        [HttpPost(nameof(DeleteCalculationTopic))]
        public async Task<IActionResult> DeleteCalculationTopic(string topicName)
        {
            var result = await _packGenerator.DeleteCalcTopic(topicName);

            if (result)
            {
                return Ok(result);
            }

            return NotFound(result);

        }

    }
}
