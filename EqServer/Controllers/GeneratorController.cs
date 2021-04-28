using EqServer.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using EqServer.EqModels.Models;

namespace EqServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeneratorController : ControllerBase
    {

        private readonly ILogger<GeneratorController> _logger;
        private readonly IPackGenerator _packGenerator;
        private readonly ICalculationPackService _calculationPackService;

        public GeneratorController(ILogger<GeneratorController> logger, IPackGenerator packGenerator, ICalculationPackService calculationPackService)
        {
            _logger = logger;
            _packGenerator = packGenerator;
            _calculationPackService = calculationPackService;
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

        [HttpPost(nameof(CreateSinglePack))]
        public async Task<IActionResult> CreateSinglePack(CalculationPack calc)
        {
            var result = await _calculationPackService.Create(calc);

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpGet(nameof(GetCalcPack))]
        public async Task<IActionResult> GetCalcPack(int id)
        {
            var result = await _calculationPackService.GetById(id);

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }



    }
}
