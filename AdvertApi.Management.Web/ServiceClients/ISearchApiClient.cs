using System.Collections.Generic;
using System.Threading.Tasks;
using AdvertApi.Management.Web.Models;

namespace AdvertApi.Management.Web.ServiceClients
{
    public interface ISearchApiClient
    {
        Task<List<AdvertType>> Search(string keyword);
    }
}