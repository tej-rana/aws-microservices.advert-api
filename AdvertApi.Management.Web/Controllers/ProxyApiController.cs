using System.Threading.Tasks;
using AdvertApi.Management.Web.ServiceClients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvertApi.Management.Web.Controllers
{
    [Route("api")]
    [ApiController]
    [Produces("application/json")]
    public class ProxyApiController : Controller
    {
        private readonly IAdvertApiClient _advertApiClient;

        public ProxyApiController(IAdvertApiClient advertApiClient)
        {
            _advertApiClient = advertApiClient;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetAsync(string id)
        {
            var record = await _advertApiClient.GetAsync(id);
            return Json(record);
        }
    }
}