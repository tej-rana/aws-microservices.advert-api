using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AdvertApi.Management.Web.Models;
using AdvertApi.Management.Web.Models.Home;
using AdvertApi.Management.Web.ServiceClients;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace AdvertApi.Management.Web.Controllers
{
    public class HomeController : Controller
    {
        public ISearchApiClient SearchApiClient { get; }
        public IMapper Mapper { get; }
        public IAdvertApiClient ApiClient { get; }

        private readonly ILogger<HomeController> _logger;

        public HomeController(ISearchApiClient searchApiClient, IMapper mapper, IAdvertApiClient apiClient, ILogger<HomeController> logger)
        {
            _logger = logger;
            SearchApiClient = searchApiClient;
            Mapper = mapper;
            ApiClient = apiClient;
        }
        
        [Authorize]
        [ResponseCache(Duration = 60)]
        public async Task<IActionResult> Index()
        {
            var allAds = await ApiClient.GetAllAsync();
            var allViewModels = allAds.Select(x => Mapper.Map<IndexViewModel>(x));

            return View(allViewModels);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
        
        [HttpPost]
        public async Task<IActionResult> Search(string keyword)
        {
            var viewModel = new List<SearchViewModel>();

            var searchResult = await SearchApiClient.Search(keyword).ConfigureAwait(false);
            searchResult.ForEach(advertDoc =>
            {
                var viewModelItem = Mapper.Map<SearchViewModel>(advertDoc);
                viewModel.Add(viewModelItem);
            });

            return View("Search", viewModel);
        }
    }
}