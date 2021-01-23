using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AdvertApi.JwtExample.Controllers
{
    [Route("api")]
    [ApiController]
    public class ValuesController : Controller
    {
        [HttpGet]
        [Authorize]
        [EnableCors("Default")]
        public ActionResult<string> Get()
        {
            return "Hello";
        }
    }
}