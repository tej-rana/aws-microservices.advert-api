using System.Threading.Tasks;
using AdvertApi.Management.Web.Models;

namespace AdvertApi.Management.Web.ServiceClients
{
    public interface IAdvertApiClient
    {
        Task<AdvertResponse> Create(CreateAdvertModel model);
    }
}