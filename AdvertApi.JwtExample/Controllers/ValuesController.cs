using Microsoft.AspNetCore.Mvc;

namespace AdvertApi.JwtExample.Controllers
{
    public class ValuesController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}