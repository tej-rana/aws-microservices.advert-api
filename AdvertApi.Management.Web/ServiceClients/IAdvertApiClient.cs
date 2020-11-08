using System.Threading.Tasks;
using AdvertApi.Management.Web.Models;
using AdvertApi.Models;

namespace AdvertApi.Management.Web.ServiceClients
{
    public interface IAdvertApiClient
    {
        Task<AdvertResponse> Create(CreateAdvertModel model);
        Task<bool> Confirm(ConfirmAdvertRequest model);
    }
}