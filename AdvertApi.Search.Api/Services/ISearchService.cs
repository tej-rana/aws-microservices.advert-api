using System.Collections.Generic;
using System.Threading.Tasks;
using AdvertApi.Search.Api.Models;

namespace AdvertApi.Search.Api.Services
{
    public interface ISearchService
    {
        Task<List<AdvertType>> Search(string keyword);
    }
}