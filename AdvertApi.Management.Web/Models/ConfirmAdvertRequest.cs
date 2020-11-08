using AdvertApi.Models;

namespace AdvertApi.Management.Web.Models
{
    public class ConfirmAdvertRequest
    {
        public string Id { get; set; }
        public AdvertStatus Status { get; set; }
    }
}