using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApiAggregation.Services;

namespace ApiAggregation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AggregationController : ControllerBase
    {
        private readonly AggregationService _aggregationService;

        public AggregationController(AggregationService aggregationService)
        {
            _aggregationService = aggregationService;
        }

        // TO DO - Change parameters in a more abstract way 
        [HttpGet]
        public async Task<IActionResult> GetAggregatedData([FromQuery] string location, [FromQuery] string topic, [FromQuery] string hashtag)
        {
            var data = await _aggregationService.GetAggregatedDataAsync(location, topic, hashtag);
            return Ok(data);
        }
    }
}
