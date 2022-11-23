using Microsoft.AspNetCore.Mvc;

namespace WEPAPI_Microservice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TheGuardianController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Warrior", "GenghisKhan", "Gladiator", "Lionheart", "Spartacus", "Saladin", "Nobunaga", "Caesar", "Hannibal ", "Leonidas"
    };

        private readonly ILogger<TheGuardianController> _logger;

        public TheGuardianController(ILogger<TheGuardianController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetTheGuardian")]
        public IEnumerable<TheGuardian> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new TheGuardian
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}

