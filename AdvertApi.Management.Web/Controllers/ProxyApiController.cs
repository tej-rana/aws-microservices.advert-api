using System.Threading.Tasks;
using AdvertApi.Management.Web.ServiceClients;
using Microsoft.AspNetCore.Mvc;

namespace AdvertApi.Management.Web.Controllers
{
    [Route("api")]
    [ApiController]
    [Produces("application/json")]
    public class ProxyApi : Controller
    {
        private readonly IAdvertApiClient _advertApiClient;

        public ProxyApi(IAdvertApiClient advertApiClient)
        {
            _advertApiClient = advertApiClient;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            var record = await _advertApiClient.GetAsync(id);
            return Json(record);
        }
    }
}