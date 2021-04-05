using EqServer.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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

        [HttpPost]
        public IActionResult CreateCalculationPacks(int numOfPacks)
        {
            if (numOfPacks <= 0)
            {
                return BadRequest(numOfPacks);
            }

            _packGenerator.GeneratePacks(numOfPacks);

            return Ok(numOfPacks);
        }
    }
}
