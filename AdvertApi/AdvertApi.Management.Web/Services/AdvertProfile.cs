using AdvertApi.Management.Web.Models;
using AdvertApi.Models;
using AutoMapper;

namespace AdvertApi.Management.Web.Services
{
    public class AdvertProfile : Profile
    {
        public AdvertProfile()
        {
            CreateMap<AdvertModel, CreateAdvertModel>();
        }
    }
}