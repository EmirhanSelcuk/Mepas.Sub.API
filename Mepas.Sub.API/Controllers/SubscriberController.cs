using Mepas.Sub.API.Models;
using Mepas.Sub.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Mepas.Sub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriberController : Controller
    {
        private readonly ISubscriberService _subscriberService;

        public SubscriberController(ISubscriberService subscriberService)
        {
            _subscriberService = subscriberService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subscriber>>> GetSubscribers(
            [FromQuery] string KayitTarihiBaslangic,
            [FromQuery] string KayitTarihiBitis,
            [FromQuery] string AranmaTarihiBaslangic,
            [FromQuery] string AranmaTarihiBitis)
        {
            var result = await _subscriberService.GetSubscribersAsync(KayitTarihiBaslangic, KayitTarihiBitis, AranmaTarihiBaslangic, AranmaTarihiBitis);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Data);
        }
    }
}
