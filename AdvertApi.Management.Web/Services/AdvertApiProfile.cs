using AdvertApi.Management.Web.Models;
using AdvertApi.Management.Web.Models.Home;
using AdvertApi.Models;
using AutoMapper;

namespace AdvertApi.Management.Web.Services
{
    public class AdvertApiProfile : Profile
    {
        public AdvertApiProfile()
        {
            CreateMap<AdvertModel, Advertisement>().ReverseMap();
          
            CreateMap<Advertisement, IndexViewModel>()
                .ForMember(dest => dest.Title, src => src.MapFrom(field => field.Title))
                .ForMember(dest => dest.Image, src => src.MapFrom(field => field.FilePath));
           
            CreateMap<AdvertType, Models.Home.SearchViewModel>()
                .ForMember(dest => dest.Title, src => src.MapFrom(field => field.Title))
                .ForMember(dest => dest.Id, src => src.MapFrom(field => field.Id));
            
            CreateMap<AdvertModel, CreateAdvertModel>().ReverseMap();
            CreateMap<CreateAdvertResponse, AdvertResponse>().ReverseMap();
            CreateMap<ConfirmAdvertRequest, ConfirmAdvertModel>().ReverseMap();
            CreateMap<CreateAdvertViewModel, CreateAdvertModel>().ReverseMap();
        }
    }
}